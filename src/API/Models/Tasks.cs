namespace API.Models
{
    public sealed class Tasks
    {
        public Guid Id { get; set; }

        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;

        public ICollection<ServiceTasks> ServiceTasks { get; set; } = new List<ServiceTasks>();
    }
}
