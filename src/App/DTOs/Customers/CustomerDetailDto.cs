namespace App.DTOs.Customers;

public sealed record CustomerDetailDto(
    CustomerDto Customer,
    IReadOnlyList<CustomerOwnedVehicleDto> OwnedVehicles,
    IReadOnlyList<CustomerRegisteredVehicleDto> RegisteredVehicles
);

public sealed record CustomerOwnedVehicleDto(
    Guid VehicleId,
    string Vin,
    string VehicleDisplay,
    DateOnly ValidFrom,
    DateOnly? ValidTo,
    bool IsCurrent
);

public sealed record CustomerRegisteredVehicleDto(
    Guid RegistrationId,
    Guid VehicleId,
    string Vin,
    string VehicleDisplay,
    string? RegistrationNo,
    string? PlateNumber,
    DateOnly ValidFrom,
    DateOnly? ValidTo,
    bool IsCurrent
);
