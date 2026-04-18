namespace API.Models
{
    public sealed class VehicleOwners
    {
        public Guid VehicleId { get; set; }
        public Guid PartyId { get; set; }

        public DateOnly ValidFrom { get; set; }
        public DateOnly? ValidTo { get; set; }

        public Vehicles Vehicle { get; set; } = null!;
        public Party Party { get; set; } = null!;
    }
}

