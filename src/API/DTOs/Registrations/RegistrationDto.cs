namespace API.DTOs.Registrations;

public sealed record RegistrationDto(
    Guid Id,
    Guid VehicleId,
    string VehicleVin,
    string VehicleDisplay,
    Guid CustomerId,
    string CustomerDisplay,
    string? RegistrationNo,
    string? PlateNumber,
    DateOnly ValidFrom,
    DateOnly? ValidTo,
    bool IsCurrent,
    string? Notes
);
