namespace API.DTOs.Vehicles;

public sealed record VehicleDetailDto(
    VehicleDto Vehicle,
    IReadOnlyList<VehicleOwnerHistoryDto> OwnershipHistory,
    IReadOnlyList<VehicleRegistrationHistoryDto> RegistrationHistory
);

public sealed record VehicleOwnerHistoryDto(
    Guid Id,
    Guid PartyId,
    string PartyDisplay,
    DateOnly ValidFrom,
    DateOnly? ValidTo,
    bool IsCurrent
);

public sealed record VehicleRegistrationHistoryDto(
    Guid Id,
    Guid PartyId,
    string PartyDisplay,
    string? RegistrationNo,
    string? PlateNo,
    DateOnly ValidFrom,
    DateOnly? ValidTo,
    bool IsCurrent
);

