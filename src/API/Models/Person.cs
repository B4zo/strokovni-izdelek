namespace API.Models
{
public sealed class Person
{
    public Guid PartyId { get; set; }

    public string FullName { get; set; } = null!;
    public DateOnly? DateOfBirth { get; set; }
    public string? TaxNo { get; set; }
    public string? Emso { get; set; }

    public Party Party { get; set; } = null!;
}
}

