namespace API.Models;

public sealed class Homologation
{
    public Guid Id { get; set; }
    public Guid VisitOperationId { get; set; }
    public VisitOperation VisitOperation { get; set; } = null!;
    public Guid VehicleId { get; set; }
    public Vehicle Vehicle { get; set; } = null!;
    public Guid? HandledByUserId { get; set; }
    public Users? HandledByUser { get; set; }
    public string Kind { get; set; } = null!;
    public string? DocumentNo { get; set; }
    public DateOnly? IssuedAt { get; set; }
    public DateOnly? ValidUntil { get; set; }
    public string? Notes { get; set; }
}
