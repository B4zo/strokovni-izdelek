namespace App.DTOs.Customers;

public sealed partial record CustomerDto(
    Guid Id,
    string Type,
    string? Address,
    string? Phone,
    string? Email,
    DateTimeOffset CreatedAt,
    PersonDto? Person,
    CompanyDto? Company
);

public sealed partial record CustomerDto
{
    public string DisplayName => Type == "person"
        ? Person?.FullName ?? "(oseba)"
        : Company?.CompanyName ?? "(firma)";

    public DateOnly? DateOfBirth => Person?.DateOfBirth;

    public string? TaxNumber => Type == "person"
        ? Person?.TaxNumber
        : Company?.TaxNumber;

    public string? NationalNo => Person?.NationalNo;

    public string? RegistrationNo => Company?.RegistrationNo;
}

public sealed record PersonDto(
    Guid CustomerId,
    string FullName,
    DateOnly? DateOfBirth,
    string? TaxNumber,
    string? NationalNo
);

public sealed record CompanyDto(
    Guid CustomerId,
    string CompanyName,
    string? TaxNumber,
    string? RegistrationNo
);
