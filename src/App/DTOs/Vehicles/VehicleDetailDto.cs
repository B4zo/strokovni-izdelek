namespace App.DTOs.Vehicles;

public sealed record VehicleDetailDto(
    VehicleDto Vehicle,
    IReadOnlyList<VehicleOwnerHistoryDto> OwnershipHistory,
    IReadOnlyList<VehicleRegistrationHistoryDto> RegistrationHistory
);

public sealed record VehicleOwnerHistoryDto(
    Guid Id,
    Guid CustomerId,
    string CustomerDisplay,
    DateOnly ValidFrom,
    DateOnly? ValidTo,
    bool IsCurrent
);

public sealed record VehicleRegistrationHistoryDto(
    Guid Id,
    Guid CustomerId,
    string CustomerDisplay,
    string? RegistrationNo,
    string? PlateNumber,
    DateOnly ValidFrom,
    DateOnly? ValidTo,
    bool IsCurrent
);
