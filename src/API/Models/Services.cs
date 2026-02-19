namespace API.Models
{
    public sealed class Services
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }
        public Guid VehicleId { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public string Status { get; set; } = "open";

        public Guid? CreatedBy { get; set; }
        public Guid? AssignedTo { get; set; }

        public string? Notes { get; set; }

        public Customers Customer { get; set; } = null!;
        public Vehicles Vehicle { get; set; } = null!;

        public Users? CreatedByUser { get; set; }
        public Users? AssignedToUser { get; set; }

        public ICollection<ServiceTasks> ServiceTasks { get; set; } = new List<ServiceTasks>();

        public ICollection<VehicleRegistrations> VehicleRegistrations { get; set; } = new List<VehicleRegistrations>();
        public ICollection<VehicleInsurances> VehicleInsurances { get; set; } = new List<VehicleInsurances>();
        public ICollection<VehicleInspections> VehicleInspections { get; set; } = new List<VehicleInspections>();
        public ICollection<VehicleHomologations> VehicleHomologations { get; set; } = new List<VehicleHomologations>();
    }
}
