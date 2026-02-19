namespace API.Models
{
    public sealed class Companies
    {
        public Guid CustomerId { get; set; }

        public string CompanyName { get; set; } = null!;
        public string? TaxNumber { get; set; }
        public string? RegistrationNo { get; set; }

        public Customers Customer { get; set; } = null!;
    }
}
