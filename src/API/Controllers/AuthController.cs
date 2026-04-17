using API.Data;
using API.Helpers;
using API.DTOs.Auth;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;
using System.Security.Claims;

namespace API.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IConfiguration _cfg;

    public AuthController(AppDbContext db, IConfiguration cfg)
    {
        _db = db;
        _cfg = cfg;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest req)
    {
        var user = await _db.Users.SingleOrDefaultAsync(u => u.Username == req.Username);
        if (user is null || !user.IsActivated)
            return Unauthorized();

        if (!BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash))
        {
            await AppendRejectedLoginEventAsync(user.Id, req.Username);
            return Unauthorized();
        }

        var activeSessionExists = await _db.UserSessions
            .Where(s => s.UserId == user.Id && s.ExpiresAt > DateTimeOffset.UtcNow)
            .Where(s => !_db.UserSessionEvents.Any(e => e.SessionId == s.Id && (e.EventType == "logout" || e.EventType == "logout_expired")))
            .AnyAsync();

        if (activeSessionExists)
            return Conflict("User already has an active session.");

        user.LastLoginAt = DateTimeOffset.UtcNow;

        var sessionExpiresAt = TokenExpiry.NextMidnightLjubljanaUtc();
        var session = new UserSession
        {
            UserId = user.Id,
            SessionKey = Guid.NewGuid().ToString("N"),
            DeviceName = Request.Headers.UserAgent.ToString(),
            IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
            CreatedAt = DateTimeOffset.UtcNow,
            ExpiresAt = sessionExpiresAt
        };

        _db.UserSessions.Add(session);
        await _db.SaveChangesAsync();

        await AppendSessionEventAsync(session.Id, "login", user.Id, "Session created");

        var refreshToken = CreateRefreshToken();
        _db.RefreshTokens.Add(new RefreshToken
        {
            SessionId = session.Id,
            TokenHash = HashToken(refreshToken),
            CreatedAt = DateTimeOffset.UtcNow,
            ExpiresAt = sessionExpiresAt
        });

        var accessExpiresAt = TokenExpiry.PlusMinutesUtc(30);
        var accessToken = CreateAccessToken(user, session, accessExpiresAt);

        await AppendSessionEventAsync(session.Id, "token_issued", user.Id, "Access and refresh tokens issued");
        await _db.SaveChangesAsync();

        return Ok(new LoginResponse(accessToken, accessExpiresAt, refreshToken));
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshRequest req)
    {
        var hashed = HashToken(req.RefreshToken);
        var token = await _db.RefreshTokens
            .Include(x => x.Session)
            .ThenInclude(s => s.User)
            .SingleOrDefaultAsync(x => x.TokenHash == hashed && x.RevokedAt == null);

        if (token is null || token.ExpiresAt <= DateTimeOffset.UtcNow)
            return Unauthorized();

        if (token.Session.ExpiresAt <= DateTimeOffset.UtcNow)
            return Unauthorized();

        var user = token.Session.User;
        if (!user.IsActivated)
            return Unauthorized();

        var accessExpiresAt = TokenExpiry.PlusMinutesUtc(30);
        var accessToken = CreateAccessToken(user, token.Session, accessExpiresAt);

        await AppendSessionEventAsync(token.SessionId, "refresh", user.Id, "Access token refreshed");
        await AppendSessionEventAsync(token.SessionId, "token_refreshed", user.Id, "Refresh request accepted");

        return Ok(new LoginResponse(accessToken, accessExpiresAt, req.RefreshToken));
    }

    [HttpPost("logout")]
    [AllowAnonymous]
    public async Task<IActionResult> Logout(LogoutRequest req)
    {
        var hashed = HashToken(req.RefreshToken);
        var token = await _db.RefreshTokens
            .Include(x => x.Session)
            .ThenInclude(s => s.User)
            .SingleOrDefaultAsync(x => x.TokenHash == hashed && x.RevokedAt == null);

        if (token is null)
            return NoContent();

        token.RevokedAt = DateTimeOffset.UtcNow;
        await AppendSessionEventAsync(token.SessionId, "logout", token.Session.UserId, "User logged out");
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("sessions")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<SessionHistoryDto>>> Sessions()
    {
        var sessions = await _db.UserSessions
            .AsNoTracking()
            .Include(x => x.User)
            .Include(x => x.Events)
            .OrderByDescending(x => x.CreatedAt)
            .Take(100)
            .ToListAsync();

        var result = sessions.Select(s => new SessionHistoryDto(
            s.Id,
            s.SessionKey,
            s.UserId,
            s.User.Username,
            s.DeviceName,
            s.IpAddress,
            s.CreatedAt,
            s.ExpiresAt,
            s.Events
                .OrderBy(e => e.EventAt)
                .Select(e => new SessionEventDto(e.Id, e.EventType, e.EventAt, e.ActorUserId, e.Details))
                .ToList()
        ));

        return Ok(result);
    }

    private async Task AppendRejectedLoginEventAsync(Guid userId, string username)
    {
        _db.UserSessionEvents.Add(new UserSessionEvent
        {
            SessionId = await GetOrCreateAuditSessionIdAsync(userId),
            EventType = "login_rejected",
            EventAt = DateTimeOffset.UtcNow,
            ActorUserId = userId,
            Details = $"{{\"username\":\"{EscapeJson(username)}\",\"reason\":\"invalid_credentials\"}}"
        });

        await _db.SaveChangesAsync();
    }

    private async Task<Guid> GetOrCreateAuditSessionIdAsync(Guid userId)
    {
        var session = await _db.UserSessions
            .Where(s => s.UserId == userId)
            .OrderByDescending(s => s.CreatedAt)
            .FirstOrDefaultAsync();

        if (session is not null)
            return session.Id;

        session = new UserSession
        {
            UserId = userId,
            SessionKey = Guid.NewGuid().ToString("N"),
            DeviceName = Request.Headers.UserAgent.ToString(),
            IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
            CreatedAt = DateTimeOffset.UtcNow,
            ExpiresAt = DateTimeOffset.UtcNow.AddMinutes(5)
        };

        _db.UserSessions.Add(session);
        await _db.SaveChangesAsync();
        return session.Id;
    }

    private async Task AppendSessionEventAsync(Guid sessionId, string eventType, Guid? actorUserId, string? details)
    {
        _db.UserSessionEvents.Add(new UserSessionEvent
        {
            SessionId = sessionId,
            EventType = eventType,
            EventAt = DateTimeOffset.UtcNow,
            ActorUserId = actorUserId,
            Details = details is null ? null : $"{{\"message\":\"{EscapeJson(details)}\"}}"
        });
        await _db.SaveChangesAsync();
    }

    private string CreateAccessToken(Users user, UserSession session, DateTimeOffset expiresAt)
    {
        var key = _cfg["Jwt:Key"]!;
        var issuer = _cfg["Jwt:Issuer"]!;
        var audience = _cfg["Jwt:Audience"]!;

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
            new Claim("sid", session.SessionKey),
            new Claim("role", user.Role)
        };

        var descriptor = new SecurityTokenDescriptor
        {
            Issuer = issuer,
            Audience = audience,
            Subject = new ClaimsIdentity(claims),
            Expires = expiresAt.UtcDateTime,
            SigningCredentials = creds
        };

        return new JsonWebTokenHandler().CreateToken(descriptor);
    }

    private string CreateRefreshToken()
    {
        Span<byte> bytes = stackalloc byte[64];
        RandomNumberGenerator.Fill(bytes);
        return Convert.ToBase64String(bytes);
    }

    private static string HashToken(string token)
    {
        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(token));
        return Convert.ToHexString(hash);
    }

    private static string EscapeJson(string value)
        => value.Replace("\\", "\\\\").Replace("\"", "\\\"");
}
