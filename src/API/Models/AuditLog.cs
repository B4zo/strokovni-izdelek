namespace API.Models;

public sealed class AuditLog
{
    public Guid Id { get; set; }
    public string EntityName { get; set; } = null!;
    public Guid EntityId { get; set; }
    public string Action { get; set; } = null!;
    public DateTimeOffset ChangedAt { get; set; }
    public Guid? ChangedByUserId { get; set; }
    public Users? ChangedByUser { get; set; }
    public Guid? VisitId { get; set; }
    public Visit? Visit { get; set; }
    public string? Details { get; set; }
}
