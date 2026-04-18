namespace API.Models
{
    public sealed class Company
    {
        public Guid PartyId { get; set; }

        public string CompanyName { get; set; } = null!;
        public string? TaxNo { get; set; }
        public string? CompanyRegNo { get; set; }

        public Party Party { get; set; } = null!;
    }
}

