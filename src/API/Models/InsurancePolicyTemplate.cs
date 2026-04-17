namespace API.Models;

public sealed class InsurancePolicyTemplate
{
    public Guid Id { get; set; }
    public Guid InsurerId { get; set; }
    public Insurer Insurer { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Scope { get; set; }
    public bool Active { get; set; } = true;

    public ICollection<InsurancePolicy> InsurancePolicies { get; set; } = new List<InsurancePolicy>();
}
