namespace API.DTOs.Vehicles;

public sealed record VehicleDto(
    Guid Id,
    string Vin,
    Guid CategoryId,
    string CategoryCode,
    string CategoryLabel,
    string? Make,
    string? Model,
    int? Year,
    string? Notes,
    string? CurrentOwner,
    string? CurrentRegistrationNo,
    string? CurrentPlate
);
