namespace API.DTOs.Parties;

public sealed record PartyDetailDto(
    PartyDto Party,
    IReadOnlyList<PartyOwnedVehicleDto> OwnedVehicles,
    IReadOnlyList<PartyRegisteredVehicleDto> RegisteredVehicles
);

public sealed record PartyOwnedVehicleDto(
    Guid VehicleId,
    string Vin,
    string VehicleDisplay,
    DateOnly ValidFrom,
    DateOnly? ValidTo,
    bool IsCurrent
);

public sealed record PartyRegisteredVehicleDto(
    Guid RegistrationId,
    Guid VehicleId,
    string Vin,
    string VehicleDisplay,
    string? RegistrationNo,
    string? PlateNo,
    DateOnly ValidFrom,
    DateOnly? ValidTo,
    bool IsCurrent
);

