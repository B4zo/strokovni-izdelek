namespace API.Models
{
    public sealed class VehicleHomologations
    {
        public Guid Id { get; set; }

        public Guid VehicleId { get; set; }
        public Guid? ServiceId { get; set; }

        public Guid? HandledBy { get; set; }

        public string Kind { get; set; } = null!;

        public string? DocumentNo { get; set; }
        public DateOnly? IssuedAt { get; set; }
        public DateOnly? ValidUntil { get; set; }

        public string? Notes { get; set; }

        public Vehicles Vehicle { get; set; } = null!;
        public Services? Service { get; set; }
        public Users? HandledByUser { get; set; }
    }
}
