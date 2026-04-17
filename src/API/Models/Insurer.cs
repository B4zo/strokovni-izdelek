namespace API.Models;

public sealed class Insurer
{
    public Guid Id { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public bool Active { get; set; } = true;

    public ICollection<InsurancePolicyTemplate> PolicyTemplates { get; set; } = new List<InsurancePolicyTemplate>();
    public ICollection<InsurancePolicy> InsurancePolicies { get; set; } = new List<InsurancePolicy>();
}
