namespace API.Models;

public sealed class VehicleCategory
{
    public Guid Id { get; set; }
    public string Code { get; set; } = null!;
    public string Label { get; set; } = null!;
    public string CategoryGroup { get; set; } = null!;
    public string? Description { get; set; }

    public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
