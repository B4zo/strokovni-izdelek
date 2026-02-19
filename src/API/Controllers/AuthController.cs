using API.Data;
using API.Models;
using API.Helpers;
using API.DTOs.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IConfiguration _cfg;
        private readonly PasswordHasher<Users> _hasher = new();

        public AuthController(AppDbContext db, IConfiguration cfg)
        {
            _db = db;
            _cfg = cfg;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(API.DTOs.Auth.LoginRequest req)
        {
            var user = await _db.Users.SingleOrDefaultAsync(u => u.Username == req.Username);

            if (user is null || !user.IsActivated)
                return Unauthorized();

            if (!BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash))
                return Unauthorized();

            user.LastLoginAt = DateTimeOffset.UtcNow;
            await _db.SaveChangesAsync();

            var expiresAt = TokenExpiry.NextMidnightLjubljanaUtc();
            var token = CreateToken(user, expiresAt);

            return Ok(new LoginResponse(token, expiresAt));
        }

        private string CreateToken(Users user, DateTimeOffset expiresAt)
        {
            var key = _cfg["Jwt:Key"]!;
            var issuer = _cfg["Jwt:Issuer"]!;
            var audience = _cfg["Jwt:Audience"]!;

            var signingKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key));

            var creds = new SigningCredentials(
                signingKey,
                SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Username)
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
    }
}
