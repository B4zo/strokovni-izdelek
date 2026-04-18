namespace API.DTOs.Parties;

public sealed record PartyDto(
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

