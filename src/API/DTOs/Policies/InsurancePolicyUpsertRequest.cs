namespace API.DTOs.Policies;

public sealed record InsurancePolicyUpsertRequest(
    Guid VisitOperationId,
    Guid VehicleId,
    Guid PartyId,
    Guid InsurerId,
    Guid TemplateId,
    string? PolicyNo,
    DateOnly ValidFrom,
    DateOnly? ValidTo,
    decimal? PremiumAmount,
    string Currency,
    string? Notes
);

