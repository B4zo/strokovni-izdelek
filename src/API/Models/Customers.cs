namespace API.Models
{
    public sealed class Customers
    {
        public Guid Id { get; set; }
        public string Type { get; set; } = null!;

        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public People? Person { get; set; }
        public Companies? Company { get; set; }

        public ICollection<VehicleOwnership> VehicleOwnerships { get; set; } = new List<VehicleOwnership>();
        public ICollection<Visit> Visits { get; set; } = new List<Visit>();
    }
}
