namespace API.Models;

public sealed class InsurancePolicy
{
    public Guid Id { get; set; }
    public Guid VisitOperationId { get; set; }
    public VisitOperation VisitOperation { get; set; } = null!;
    public Guid VehicleId { get; set; }
    public Vehicle Vehicle { get; set; } = null!;
    public Guid PartyId { get; set; }
    public Party Party { get; set; } = null!;
    public Guid InsurerId { get; set; }
    public Insurer Insurer { get; set; } = null!;
    public Guid TemplateId { get; set; }
    public InsurancePolicyTemplate Template { get; set; } = null!;
    public string? PolicyNo { get; set; }
    public DateOnly ValidFrom { get; set; }
    public DateOnly? ValidTo { get; set; }
    public decimal? PremiumAmount { get; set; }
    public string Currency { get; set; } = "EUR";
    public string? Notes { get; set; }
}

