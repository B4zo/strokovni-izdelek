namespace API.Models;

public sealed class Visit
{
    public Guid Id { get; set; }
    public Guid PartyId { get; set; }
    public Party Party { get; set; } = null!;
    public Guid? VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }
    public DateTimeOffset VisitedAt { get; set; }
    public Guid? HandledByUserId { get; set; }
    public Users? HandledByUser { get; set; }
    public string? Notes { get; set; }

    public ICollection<VisitOperation> Operations { get; set; } = new List<VisitOperation>();
    public ICollection<AuditLog> AuditEntries { get; set; } = new List<AuditLog>();
}

