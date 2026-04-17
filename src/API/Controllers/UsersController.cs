using API.Data;
using API.DTOs.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/users")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _db;

    public UsersController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> Get(
        [FromQuery] string? q = null,
        [FromQuery] string? role = null,
        [FromQuery] bool? isActive = null)
    {
        var query = _db.Users.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(q))
        {
            q = q.Trim();
            query = query.Where(x =>
                EF.Functions.ILike(x.Username, $"%{q}%") ||
                EF.Functions.ILike(x.DisplayName, $"%{q}%"));
        }

        if (!string.IsNullOrWhiteSpace(role))
        {
            role = role.Trim().ToLowerInvariant();
            query = query.Where(x => x.Role == role);
        }

        if (isActive is not null)
            query = query.Where(x => x.IsActivated == isActive.Value);

        var items = await query
            .OrderBy(x => x.Username)
            .Select(x => new UserDto(
                x.Id,
                x.Username,
                x.DisplayName,
                x.Role,
                x.IsActivated,
                x.CreatedAt,
                x.LastLoginAt))
            .ToListAsync();

        return Ok(items);
    }
}
