namespace API.Models
{
    public sealed class VehicleInsurances
    {
        public Guid Id { get; set; }

        public Guid VehicleId { get; set; }
        public Guid? ServiceId { get; set; }

        public string InsurerName { get; set; } = null!;
        public string? PolicyNumber { get; set; }

        public DateOnly ValidFrom { get; set; }
        public DateOnly ValidTo { get; set; }

        public string? CoverageType { get; set; }
        public string? Notes { get; set; }

        public Vehicles Vehicle { get; set; } = null!;
        public Services? Service { get; set; }
    }
}
