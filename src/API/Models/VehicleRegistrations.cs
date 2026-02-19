namespace API.Models
{
    public sealed class VehicleRegistrations
    {
        public Guid Id { get; set; }

        public Guid VehicleId { get; set; }
        public Guid? ServiceId { get; set; }

        public DateOnly ValidFrom { get; set; }
        public DateOnly ValidTo { get; set; }

        public string? PlateNumber { get; set; }
        public string? Notes { get; set; }

        public Vehicles Vehicle { get; set; } = null!;
        public Services? Service { get; set; }
    }
}
