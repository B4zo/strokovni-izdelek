namespace API.DTOs.Registrations;

public sealed record VehicleRegistrationUpsertRequest(
    Guid VisitOperationId,
    Guid VehicleId,
    Guid CustomerId,
    DateOnly ValidFrom,
    DateOnly? ValidTo,
    string? RegistrationNo,
    string? Notes
);
