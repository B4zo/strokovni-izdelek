namespace API.Models;

public sealed class VehicleRegistration
{
    public Guid Id { get; set; }
    public Guid VisitOperationId { get; set; }
    public VisitOperation VisitOperation { get; set; } = null!;
    public Guid VehicleId { get; set; }
    public Vehicle Vehicle { get; set; } = null!;
    public Guid CustomerId { get; set; }
    public Customers Customer { get; set; } = null!;
    public DateOnly ValidFrom { get; set; }
    public DateOnly? ValidTo { get; set; }
    public string? RegistrationNo { get; set; }
    public string? Notes { get; set; }

    public ICollection<PlateAssignment> PlateAssignments { get; set; } = new List<PlateAssignment>();
}
