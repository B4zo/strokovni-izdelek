namespace API.Models;

public sealed class PlateAssignment
{
    public Guid Id { get; set; }
    public Guid VisitOperationId { get; set; }
    public VisitOperation VisitOperation { get; set; } = null!;
    public Guid PlateId { get; set; }
    public Plate Plate { get; set; } = null!;
    public Guid RegistrationId { get; set; }
    public VehicleRegistration Registration { get; set; } = null!;
    public DateOnly ValidFrom { get; set; }
    public DateOnly? ValidTo { get; set; }
}
