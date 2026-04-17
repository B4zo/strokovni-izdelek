namespace API.Models
{
    public sealed class Users
    {
        public Guid Id { get; set; }

        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string DisplayName { get; set; } = null!;

        public string Role { get; set; } = "employee";
        public bool IsActivated { get; set; } = true;

        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? LastLoginAt { get; set; }

        public ICollection<UserSession> Sessions { get; set; } = new List<UserSession>();
        public ICollection<Visit> VisitsHandled { get; set; } = new List<Visit>();
        public ICollection<Inspection> PerformedInspections { get; set; } = new List<Inspection>();
        public ICollection<Homologation> HandledHomologations { get; set; } = new List<Homologation>();
    }
}
