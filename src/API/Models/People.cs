namespace API.Models
{
    public sealed class People
    {
        public Guid CustomerId { get; set; }

        public string FullName { get; set; } = null!;
        public DateOnly? DateOfBirth { get; set; }
        public string? TaxNumber { get; set; }
        public string? NationalNo { get; set; }

        public Customers Customer { get; set; } = null!;
    }
}
