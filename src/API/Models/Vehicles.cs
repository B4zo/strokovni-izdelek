namespace API.Models
{
    public sealed class Vehicles
    {
        public Guid Id { get; set; }

        public string? Vin { get; set; }
        public string? PlateNumber { get; set; }

        public string? Make { get; set; }
        public string? Model { get; set; }
        public int? Year { get; set; }

        public string? Category { get; set; }
        public string? Notes { get; set; }

        public ICollection<VehicleOwners> Owners { get; set; } = new List<VehicleOwners>();
        public ICollection<Services> Services { get; set; } = new List<Services>();

        public ICollection<VehicleRegistrations> Registrations { get; set; } = new List<VehicleRegistrations>();
        public ICollection<VehicleInsurances> Insurances { get; set; } = new List<VehicleInsurances>();
        public ICollection<VehicleInspections> Inspections { get; set; } = new List<VehicleInspections>();
        public ICollection<VehicleHomologations> Homologations { get; set; } = new List<VehicleHomologations>();
    }
}
