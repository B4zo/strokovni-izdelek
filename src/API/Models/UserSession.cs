namespace API.Models;

public sealed class UserSession
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Users User { get; set; } = null!;
    public string SessionKey { get; set; } = null!;
    public string? DeviceName { get; set; }
    public string? IpAddress { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset ExpiresAt { get; set; }

    public ICollection<UserSessionEvent> Events { get; set; } = new List<UserSessionEvent>();
}
