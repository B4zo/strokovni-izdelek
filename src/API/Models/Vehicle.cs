namespace API.Models;

public sealed class Vehicle
{
    public Guid Id { get; set; }
    public string Vin { get; set; } = null!;
    public Guid CategoryId { get; set; }
    public VehicleCategory Category { get; set; } = null!;
    public string? Make { get; set; }
    public string? Model { get; set; }
    public int? Year { get; set; }
    public string? Notes { get; set; }

    public ICollection<Visit> Visits { get; set; } = new List<Visit>();
    public ICollection<VehicleOwnership> OwnershipHistory { get; set; } = new List<VehicleOwnership>();
    public ICollection<VehicleRegistration> Registrations { get; set; } = new List<VehicleRegistration>();
    public ICollection<InsurancePolicy> InsurancePolicies { get; set; } = new List<InsurancePolicy>();
    public ICollection<Inspection> Inspections { get; set; } = new List<Inspection>();
    public ICollection<Homologation> Homologations { get; set; } = new List<Homologation>();
}
