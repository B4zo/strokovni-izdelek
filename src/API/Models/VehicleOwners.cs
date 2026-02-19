namespace API.Models
{
    public sealed class VehicleOwners
    {
        public Guid VehicleId { get; set; }
        public Guid CustomerId { get; set; }

        public DateOnly ValidFrom { get; set; }
        public DateOnly? ValidTo { get; set; }

        public Vehicles Vehicle { get; set; } = null!;
        public Customers Customer { get; set; } = null!;
    }
}
