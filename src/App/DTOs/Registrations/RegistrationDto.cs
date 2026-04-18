namespace App.DTOs.Registrations;

public sealed record RegistrationDto(
    Guid Id,
    Guid VehicleId,
    string VehicleVin,
    string VehicleDisplay,
    Guid PartyId,
    string PartyDisplay,
    string? RegistrationNo,
    string? PlateNo,
    DateOnly ValidFrom,
    DateOnly? ValidTo,
    bool IsCurrent,
    string? Notes
);

