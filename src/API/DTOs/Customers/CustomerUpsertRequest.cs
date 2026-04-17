namespace API.DTOs.Customers;

public sealed record CustomerUpsertRequest(
    string Type,
    string? Address,
    string? Phone,
    string? Email,
    string? FullName,
    DateOnly? DateOfBirth,
    string? TaxNumber,
    string? NationalNo,
    string? CompanyName,
    string? RegistrationNo
);
