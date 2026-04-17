namespace API.Models;

public sealed class Plate
{
    public Guid Id { get; set; }
    public string PlateNumber { get; set; } = null!;
    public string RegionCode { get; set; } = null!;
    public bool Active { get; set; } = true;
    public string? Notes { get; set; }

    public ICollection<PlateAssignment> Assignments { get; set; } = new List<PlateAssignment>();
}
