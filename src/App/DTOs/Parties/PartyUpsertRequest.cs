namespace App.DTOs.Parties;

public sealed record PartyUpsertRequest(
    string Type,
    string? Address,
    string? Phone,
    string? Email,
    string? FullName,
    DateOnly? DateOfBirth,
    string? TaxNo,
    string? Emso,
    string? CompanyName,
    string? CompanyRegNo
);

