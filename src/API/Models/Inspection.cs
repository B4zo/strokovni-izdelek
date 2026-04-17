namespace API.Models;

public sealed class Inspection
{
    public Guid Id { get; set; }
    public Guid VisitOperationId { get; set; }
    public VisitOperation VisitOperation { get; set; } = null!;
    public Guid VehicleId { get; set; }
    public Vehicle Vehicle { get; set; } = null!;
    public Guid? PerformedByUserId { get; set; }
    public Users? PerformedByUser { get; set; }
    public DateTimeOffset PerformedAt { get; set; }
    public string Result { get; set; } = "pending";
    public bool Finished { get; set; }
    public DateOnly? ValidUntil { get; set; }
    public int? OdometerKm { get; set; }
    public string? Notes { get; set; }
}
