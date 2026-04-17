namespace API.DTOs.Customers;

public sealed record CustomerDto(
    Guid Id,
    string Type,
    string? Address,
    string? Phone,
    string? Email,
    DateTimeOffset CreatedAt,
    PersonDto? Person,
    CompanyDto? Company
);

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
