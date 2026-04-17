namespace API.Models;

public sealed class UserSessionEvent
{
    public Guid Id { get; set; }
    public Guid SessionId { get; set; }
    public UserSession Session { get; set; } = null!;
    public string EventType { get; set; } = null!;
    public DateTimeOffset EventAt { get; set; }
    public Guid? ActorUserId { get; set; }
    public Users? ActorUser { get; set; }
    public string? Details { get; set; }
}
