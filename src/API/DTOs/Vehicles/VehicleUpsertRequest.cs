namespace API.DTOs.Vehicles;

public sealed record VehicleUpsertRequest(
    string Vin,
    Guid CategoryId,
    string? Make,
    string? Model,
    int? Year,
    string? Notes
);
