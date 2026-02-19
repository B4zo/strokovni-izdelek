namespace API.Models
{
    public sealed class Users
    {
        public Guid Id { get; set; }

        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string DisplayName { get; set; } = null!;

        public string Role { get; set; } = "employee";
        public bool IsActivated { get; set; } = true;

        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? LastLoginAt { get; set; }

        public ICollection<Services> CreatedServices { get; set; } = new List<Services>();
        public ICollection<Services> AssignedServices { get; set; } = new List<Services>();

        public ICollection<ServiceTasks> PerformedServiceTasks { get; set; } = new List<ServiceTasks>();

        public ICollection<VehicleInspections> PerformedInspections { get; set; } = new List<VehicleInspections>();
        public ICollection<VehicleHomologations> HandledHomologations { get; set; } = new List<VehicleHomologations>();
    }
}
