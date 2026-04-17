namespace API.Models;

public sealed class VisitOperation
{
    public Guid Id { get; set; }
    public Guid VisitId { get; set; }
    public Visit Visit { get; set; } = null!;
    public string OpType { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public string? Notes { get; set; }

    public ICollection<VehicleOwnership> OwnershipChanges { get; set; } = new List<VehicleOwnership>();
    public ICollection<VehicleRegistration> Registrations { get; set; } = new List<VehicleRegistration>();
    public ICollection<PlateAssignment> PlateAssignments { get; set; } = new List<PlateAssignment>();
    public ICollection<InsurancePolicy> InsurancePolicies { get; set; } = new List<InsurancePolicy>();
    public ICollection<Inspection> Inspections { get; set; } = new List<Inspection>();
    public ICollection<Homologation> Homologations { get; set; } = new List<Homologation>();
}
