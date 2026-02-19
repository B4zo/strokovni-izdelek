namespace API.Models
{
    public sealed class VehicleInspections
    {
        public Guid Id { get; set; }

        public Guid VehicleId { get; set; }
        public Guid? ServiceId { get; set; }

        public Guid? PerformedBy { get; set; }
        public DateTimeOffset PerformedAt { get; set; }

        public string Result { get; set; } = null!;
        public DateOnly? ValidUntil { get; set; }

        public int? OdometerKm { get; set; }
        public string? Notes { get; set; }

        public Vehicles Vehicle { get; set; } = null!;
        public Services? Service { get; set; }
        public Users? PerformedByUser { get; set; }
    }
}
