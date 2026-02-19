namespace API.Models
{
    public sealed class ServiceTasks
    {
        public Guid ServiceId { get; set; }
        public Guid TaskId { get; set; }

        public string Status { get; set; } = "pending";

        public Guid? PerformedBy { get; set; }
        public DateTimeOffset? PerformedAt { get; set; }

        public string? Notes { get; set; }

        public Services Service { get; set; } = null!;
        public Tasks Task { get; set; } = null!;

        public Users? PerformedByUser { get; set; }
    }
}
