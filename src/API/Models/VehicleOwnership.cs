namespace API.Models;

public sealed class VehicleOwnership
{
    public Guid Id { get; set; }
    public Guid VisitOperationId { get; set; }
    public VisitOperation VisitOperation { get; set; } = null!;
    public Guid VehicleId { get; set; }
    public Vehicle Vehicle { get; set; } = null!;
    public Guid PartyId { get; set; }
    public Party Party { get; set; } = null!;
    public DateOnly ValidFrom { get; set; }
    public DateOnly? ValidTo { get; set; }
}

