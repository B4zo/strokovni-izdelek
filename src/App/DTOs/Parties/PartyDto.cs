namespace App.DTOs.Parties;

public sealed partial record PartyDto(
    Guid Id,
    string Type,
    string? Address,
    string? Phone,
    string? Email,
    DateTimeOffset CreatedAt,
    PersonDto? Person,
    CompanyDto? Company
);

public sealed partial record PartyDto
{
    public string DisplayName => Type == "person"
        ? Person?.FullName ?? "(oseba)"
        : Company?.CompanyName ?? "(firma)";

    public DateOnly? DateOfBirth => Person?.DateOfBirth;

    public string? TaxNo => Type == "person"
        ? Person?.TaxNo
        : Company?.TaxNo;

    public string? Emso => Person?.Emso;

    public string? CompanyRegNo => Company?.CompanyRegNo;
}

public sealed record PersonDto(
    Guid PartyId,
    string FullName,
    DateOnly? DateOfBirth,
    string? TaxNo,
    string? Emso
);

public sealed record CompanyDto(
    Guid PartyId,
    string CompanyName,
    string? TaxNo,
    string? CompanyRegNo
);

