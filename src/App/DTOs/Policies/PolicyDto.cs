namespace App.DTOs.Policies;

public sealed record PolicyDto(
    Guid Id,
    Guid VehicleId,
    string VehicleVin,
    string VehicleDisplay,
    Guid CustomerId,
    string CustomerDisplay,
    Guid InsurerId,
    string InsurerName,
    Guid TemplateId,
    string TemplateName,
    string? PolicyNo,
    DateOnly ValidFrom,
    DateOnly? ValidTo,
    bool IsCurrent,
    decimal? PremiumAmount,
    string Currency,
    string? Notes
);
