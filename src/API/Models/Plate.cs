namespace API.Models;

public sealed class Plate
{
    public Guid Id { get; set; }
    public string PlateNo { get; set; } = null!;
    public string PlateType { get; set; } = null!;

    public ICollection<PlateAssignment> Assignments { get; set; } = new List<PlateAssignment>();
}
