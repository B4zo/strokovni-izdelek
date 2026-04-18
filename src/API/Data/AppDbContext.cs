using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Party> Parties => Set<Party>();
    public DbSet<Person> People => Set<Person>();
    public DbSet<Company> Companies => Set<Company>();
    public DbSet<Users> Users => Set<Users>();
    public DbSet<UserSession> UserSessions => Set<UserSession>();
    public DbSet<UserSessionEvent> UserSessionEvents => Set<UserSessionEvent>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    public DbSet<VehicleCategory> VehicleCategories => Set<VehicleCategory>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<VehicleOwnership> VehicleOwnerships => Set<VehicleOwnership>();
    public DbSet<VehicleRegistration> VehicleRegistrations => Set<VehicleRegistration>();
    public DbSet<Plate> Plates => Set<Plate>();
    public DbSet<PlateAssignment> PlateAssignments => Set<PlateAssignment>();
    public DbSet<Insurer> Insurers => Set<Insurer>();
    public DbSet<InsurancePolicyTemplate> InsurancePolicyTemplates => Set<InsurancePolicyTemplate>();
    public DbSet<InsurancePolicy> InsurancePolicies => Set<InsurancePolicy>();
    public DbSet<Inspection> Inspections => Set<Inspection>();
    public DbSet<Homologation> Homologations => Set<Homologation>();
    public DbSet<Visit> Visits => Set<Visit>();
    public DbSet<VisitOperation> VisitOperations => Set<VisitOperation>();
    public DbSet<AuditLog> AuditLog => Set<AuditLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tehnicni");
        modelBuilder.HasPostgresExtension("pgcrypto");

        modelBuilder.Entity<Party>(e =>
        {
            e.ToTable("parties", tb => tb.HasCheckConstraint("parties_type_check", "\"type\" IN ('person','company')"));
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id").HasDefaultValueSql("gen_random_uuid()").ValueGeneratedOnAdd();
            e.Property(x => x.Type).HasColumnName("type").IsRequired();
            e.Property(x => x.Address).HasColumnName("address");
            e.Property(x => x.Phone).HasColumnName("phone");
            e.Property(x => x.Email).HasColumnName("email");
            e.Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("now()").ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Person>(e =>
        {
            e.ToTable("people", tb =>
            {
                tb.HasCheckConstraint("people_tax_no_format_check", "\"tax_no\" IS NULL OR \"tax_no\" ~ '^[0-9]{8}$'");
                tb.HasCheckConstraint("people_emso_format_check", "\"emso\" IS NULL OR \"emso\" ~ '^[0-9]{13}$'");
            });
            e.HasKey(x => x.PartyId);
            e.HasIndex(x => x.TaxNo).IsUnique();
            e.HasIndex(x => x.Emso).IsUnique();
            e.Property(x => x.PartyId).HasColumnName("party_id");
            e.Property(x => x.FullName).HasColumnName("full_name").IsRequired();
            e.Property(x => x.DateOfBirth).HasColumnName("date_of_birth");
            e.Property(x => x.TaxNo).HasColumnName("tax_no").HasMaxLength(8);
            e.Property(x => x.Emso).HasColumnName("emso").HasMaxLength(13);
        });

        modelBuilder.Entity<Company>(e =>
        {
            e.ToTable("companies", tb =>
            {
                tb.HasCheckConstraint("companies_tax_no_format_check", "\"tax_no\" IS NULL OR \"tax_no\" ~ '^[0-9]{8}$'");
                tb.HasCheckConstraint("companies_company_reg_no_format_check", "\"company_reg_no\" IS NULL OR \"company_reg_no\" ~ '^[0-9]{8}$'");
            });
            e.HasKey(x => x.PartyId);
            e.HasIndex(x => x.TaxNo).IsUnique();
            e.HasIndex(x => x.CompanyRegNo).IsUnique();
            e.Property(x => x.PartyId).HasColumnName("party_id");
            e.Property(x => x.CompanyName).HasColumnName("company_name").IsRequired();
            e.Property(x => x.TaxNo).HasColumnName("tax_no").HasMaxLength(8);
            e.Property(x => x.CompanyRegNo).HasColumnName("company_reg_no").HasMaxLength(8);
        });

        modelBuilder.Entity<Users>(e =>
        {
            e.ToTable("users", tb => tb.HasCheckConstraint("users_role_check", "\"role\" IN ('employee','admin','manager','office','mechanic')"));
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id").HasDefaultValueSql("gen_random_uuid()").ValueGeneratedOnAdd();
            e.Property(x => x.Username).HasColumnName("username").IsRequired();
            e.HasIndex(x => x.Username).IsUnique();
            e.Property(x => x.PasswordHash).HasColumnName("password_hash").IsRequired();
            e.Property(x => x.DisplayName).HasColumnName("display_name").IsRequired();
            e.Property(x => x.Role).HasColumnName("role").HasDefaultValue("employee").IsRequired();
            e.Property(x => x.IsActivated).HasColumnName("is_activated").HasDefaultValue(true).IsRequired();
            e.Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("now()").ValueGeneratedOnAdd();
            e.Property(x => x.LastLoginAt).HasColumnName("last_login_at");
        });

        modelBuilder.Entity<UserSession>(e =>
        {
            e.ToTable("user_sessions");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id").HasDefaultValueSql("gen_random_uuid()").ValueGeneratedOnAdd();
            e.Property(x => x.UserId).HasColumnName("user_id").IsRequired();
            e.Property(x => x.SessionKey).HasColumnName("session_key").IsRequired();
            e.HasIndex(x => x.SessionKey).IsUnique();
            e.Property(x => x.DeviceName).HasColumnName("device_name");
            e.Property(x => x.IpAddress).HasColumnName("ip_address");
            e.Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("now()").ValueGeneratedOnAdd();
            e.Property(x => x.ExpiresAt).HasColumnName("expires_at").IsRequired();
        });

        modelBuilder.Entity<UserSessionEvent>(e =>
        {
            e.ToTable("user_session_events", tb => tb.HasCheckConstraint("user_session_events_type_check",
                "\"event_type\" IN ('login','refresh','logout','logout_expired','login_rejected','token_issued','token_refreshed')"));
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id").HasDefaultValueSql("gen_random_uuid()").ValueGeneratedOnAdd();
            e.Property(x => x.SessionId).HasColumnName("session_id").IsRequired();
            e.Property(x => x.EventType).HasColumnName("event_type").IsRequired();
            e.Property(x => x.EventAt).HasColumnName("event_at").HasDefaultValueSql("now()").ValueGeneratedOnAdd();
            e.Property(x => x.ActorUserId).HasColumnName("actor_user_id");
            e.Property(x => x.Details).HasColumnName("details").HasColumnType("jsonb");
        });

        modelBuilder.Entity<RefreshToken>(e =>
        {
            e.ToTable("refresh_tokens");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id").HasDefaultValueSql("gen_random_uuid()").ValueGeneratedOnAdd();
            e.Property(x => x.SessionId).HasColumnName("session_id").IsRequired();
            e.Property(x => x.TokenHash).HasColumnName("token_hash").IsRequired();
            e.HasIndex(x => x.TokenHash).IsUnique();
            e.Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("now()").ValueGeneratedOnAdd();
            e.Property(x => x.ExpiresAt).HasColumnName("expires_at").IsRequired();
            e.Property(x => x.RevokedAt).HasColumnName("revoked_at");
        });

        modelBuilder.Entity<VehicleCategory>(e =>
        {
            e.ToTable("vehicle_categories", tb => tb.HasCheckConstraint("vehicle_categories_code_check",
                "\"code\" IN ('L1e','L2e','L3e','L4e','L5e','L6e','L7e','M1','M2','M3','N1','N2','N3','O1','O2','O3','O4','T1','T2','T3','T4','T5','C1','C2','C3','C4')"));
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id").HasDefaultValueSql("gen_random_uuid()").ValueGeneratedOnAdd();
            e.Property(x => x.Code).HasColumnName("code").IsRequired();
            e.HasIndex(x => x.Code).IsUnique();
            e.Property(x => x.Label).HasColumnName("label").IsRequired();
            e.Property(x => x.CategoryGroup).HasColumnName("category_group").IsRequired();
            e.Property(x => x.Description).HasColumnName("description");
        });

        modelBuilder.Entity<Vehicle>(e =>
        {
            e.ToTable("vehicles");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id").HasDefaultValueSql("gen_random_uuid()").ValueGeneratedOnAdd();
            e.Property(x => x.Vin).HasColumnName("vin").IsRequired();
            e.HasIndex(x => x.Vin).IsUnique();
            e.Property(x => x.CategoryId).HasColumnName("category_id").IsRequired();
            e.Property(x => x.Make).HasColumnName("make");
            e.Property(x => x.Model).HasColumnName("model");
            e.Property(x => x.Year).HasColumnName("year");
            e.Property(x => x.Notes).HasColumnName("notes");
        });

        modelBuilder.Entity<Visit>(e =>
        {
            e.ToTable("visits");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id").HasDefaultValueSql("gen_random_uuid()").ValueGeneratedOnAdd();
            e.Property(x => x.PartyId).HasColumnName("party_id").IsRequired();
            e.Property(x => x.VehicleId).HasColumnName("vehicle_id");
            e.Property(x => x.VisitedAt).HasColumnName("visited_at").HasDefaultValueSql("now()").ValueGeneratedOnAdd();
            e.Property(x => x.HandledByUserId).HasColumnName("handled_by_user_id");
            e.Property(x => x.Notes).HasColumnName("notes");
        });

        modelBuilder.Entity<VisitOperation>(e =>
        {
            e.ToTable("visit_operations", tb => tb.HasCheckConstraint("visit_operations_type_check",
                "\"op_type\" IN ('ownership_change','registration_issue','registration_extension','plate_assignment','insurance_issue','inspection_finish','homologation','other')"));
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id").HasDefaultValueSql("gen_random_uuid()").ValueGeneratedOnAdd();
            e.Property(x => x.VisitId).HasColumnName("visit_id").IsRequired();
            e.Property(x => x.OpType).HasColumnName("op_type").IsRequired();
            e.Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("now()").ValueGeneratedOnAdd();
            e.Property(x => x.Notes).HasColumnName("notes");
        });

        modelBuilder.Entity<VehicleOwnership>(e =>
        {
            e.ToTable("vehicle_ownerships", tb => tb.HasCheckConstraint("vehicle_ownerships_valid_range_check", "\"valid_to\" IS NULL OR \"valid_to\" >= \"valid_from\""));
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id").HasDefaultValueSql("gen_random_uuid()").ValueGeneratedOnAdd();
            e.Property(x => x.VisitOperationId).HasColumnName("visit_operation_id").IsRequired();
            e.Property(x => x.VehicleId).HasColumnName("vehicle_id").IsRequired();
            e.Property(x => x.PartyId).HasColumnName("party_id").IsRequired();
            e.Property(x => x.ValidFrom).HasColumnName("valid_from").IsRequired();
            e.Property(x => x.ValidTo).HasColumnName("valid_to");
        });

        modelBuilder.Entity<VehicleRegistration>(e =>
        {
            e.ToTable("vehicle_registrations", tb => tb.HasCheckConstraint("vehicle_registrations_valid_range_check", "\"valid_to\" IS NULL OR \"valid_to\" >= \"valid_from\""));
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id").HasDefaultValueSql("gen_random_uuid()").ValueGeneratedOnAdd();
            e.Property(x => x.VisitOperationId).HasColumnName("visit_operation_id").IsRequired();
            e.Property(x => x.VehicleId).HasColumnName("vehicle_id").IsRequired();
            e.Property(x => x.PartyId).HasColumnName("party_id").IsRequired();
            e.Property(x => x.ValidFrom).HasColumnName("valid_from").IsRequired();
            e.Property(x => x.ValidTo).HasColumnName("valid_to");
            e.Property(x => x.RegistrationNo).HasColumnName("registration_no");
            e.Property(x => x.Notes).HasColumnName("notes");
        });

        modelBuilder.Entity<Plate>(e =>
        {
            e.ToTable("plates", tb => tb.HasCheckConstraint("plates_type_check",
                "\"plate_type\" IN ('standard','custom','diplomatic','military','police','temporary','test','export','agricultural','trailer','moped')"));
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id").HasDefaultValueSql("gen_random_uuid()").ValueGeneratedOnAdd();
            e.Property(x => x.PlateNo).HasColumnName("plate_no").IsRequired();
            e.HasIndex(x => x.PlateNo).IsUnique();
            e.Property(x => x.PlateType).HasColumnName("plate_type").IsRequired();
        });

        modelBuilder.Entity<PlateAssignment>(e =>
        {
            e.ToTable("plate_assignments", tb => tb.HasCheckConstraint("plate_assignments_valid_range_check", "\"valid_to\" IS NULL OR \"valid_to\" >= \"valid_from\""));
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id").HasDefaultValueSql("gen_random_uuid()").ValueGeneratedOnAdd();
            e.Property(x => x.VisitOperationId).HasColumnName("visit_operation_id").IsRequired();
            e.Property(x => x.PlateId).HasColumnName("plate_id").IsRequired();
            e.Property(x => x.RegistrationId).HasColumnName("registration_id").IsRequired();
            e.Property(x => x.ValidFrom).HasColumnName("valid_from").IsRequired();
            e.Property(x => x.ValidTo).HasColumnName("valid_to");
        });

        modelBuilder.Entity<Insurer>(e =>
        {
            e.ToTable("insurers");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id").HasDefaultValueSql("gen_random_uuid()").ValueGeneratedOnAdd();
            e.Property(x => x.Code).HasColumnName("code").IsRequired();
            e.HasIndex(x => x.Code).IsUnique();
            e.Property(x => x.Name).HasColumnName("name").IsRequired();
            e.Property(x => x.Active).HasColumnName("active").HasDefaultValue(true).IsRequired();
        });

        modelBuilder.Entity<InsurancePolicyTemplate>(e =>
        {
            e.ToTable("insurance_policy_templates");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id").HasDefaultValueSql("gen_random_uuid()").ValueGeneratedOnAdd();
            e.Property(x => x.InsurerId).HasColumnName("insurer_id").IsRequired();
            e.Property(x => x.Code).HasColumnName("code").IsRequired();
            e.Property(x => x.Name).HasColumnName("name").IsRequired();
            e.Property(x => x.Scope).HasColumnName("scope");
            e.Property(x => x.Active).HasColumnName("active").HasDefaultValue(true).IsRequired();
            e.HasIndex(x => new { x.InsurerId, x.Code }).IsUnique();
        });

        modelBuilder.Entity<InsurancePolicy>(e =>
        {
            e.ToTable("insurance_policies", tb => tb.HasCheckConstraint("insurance_policies_valid_range_check", "\"valid_to\" IS NULL OR \"valid_to\" >= \"valid_from\""));
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id").HasDefaultValueSql("gen_random_uuid()").ValueGeneratedOnAdd();
            e.Property(x => x.VisitOperationId).HasColumnName("visit_operation_id").IsRequired();
            e.Property(x => x.VehicleId).HasColumnName("vehicle_id").IsRequired();
            e.Property(x => x.PartyId).HasColumnName("party_id").IsRequired();
            e.Property(x => x.InsurerId).HasColumnName("insurer_id").IsRequired();
            e.Property(x => x.TemplateId).HasColumnName("template_id").IsRequired();
            e.Property(x => x.PolicyNo).HasColumnName("policy_no");
            e.Property(x => x.ValidFrom).HasColumnName("valid_from").IsRequired();
            e.Property(x => x.ValidTo).HasColumnName("valid_to");
            e.Property(x => x.PremiumAmount).HasColumnName("premium_amount").HasColumnType("numeric(12,2)");
            e.Property(x => x.Currency).HasColumnName("currency").HasDefaultValue("EUR").IsRequired();
            e.Property(x => x.Notes).HasColumnName("notes");
        });

        modelBuilder.Entity<Inspection>(e =>
        {
            e.ToTable("inspections", tb => tb.HasCheckConstraint("inspections_result_check", "\"result\" IN ('pending','passed','failed','conditional')"));
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id").HasDefaultValueSql("gen_random_uuid()").ValueGeneratedOnAdd();
            e.Property(x => x.VisitOperationId).HasColumnName("visit_operation_id").IsRequired();
            e.Property(x => x.VehicleId).HasColumnName("vehicle_id").IsRequired();
            e.Property(x => x.PerformedByUserId).HasColumnName("performed_by_user_id");
            e.Property(x => x.PerformedAt).HasColumnName("performed_at").HasDefaultValueSql("now()").ValueGeneratedOnAdd();
            e.Property(x => x.Result).HasColumnName("result").IsRequired();
            e.Property(x => x.Finished).HasColumnName("finished").HasDefaultValue(false).IsRequired();
            e.Property(x => x.ValidUntil).HasColumnName("valid_until");
            e.Property(x => x.OdometerKm).HasColumnName("odometer_km");
            e.Property(x => x.Notes).HasColumnName("notes");
        });

        modelBuilder.Entity<Homologation>(e =>
        {
            e.ToTable("homologations", tb => tb.HasCheckConstraint("homologations_kind_check", "\"kind\" IN ('coc','import','modification','individual_approval','data_correction')"));
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id").HasDefaultValueSql("gen_random_uuid()").ValueGeneratedOnAdd();
            e.Property(x => x.VisitOperationId).HasColumnName("visit_operation_id").IsRequired();
            e.Property(x => x.VehicleId).HasColumnName("vehicle_id").IsRequired();
            e.Property(x => x.HandledByUserId).HasColumnName("handled_by_user_id");
            e.Property(x => x.Kind).HasColumnName("kind").IsRequired();
            e.Property(x => x.DocumentNo).HasColumnName("document_no");
            e.Property(x => x.IssuedAt).HasColumnName("issued_at");
            e.Property(x => x.ValidUntil).HasColumnName("valid_until");
            e.Property(x => x.Notes).HasColumnName("notes");
        });

        modelBuilder.Entity<AuditLog>(e =>
        {
            e.ToTable("audit_log");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id").HasDefaultValueSql("gen_random_uuid()").ValueGeneratedOnAdd();
            e.Property(x => x.EntityName).HasColumnName("entity_name").IsRequired();
            e.Property(x => x.EntityId).HasColumnName("entity_id").IsRequired();
            e.Property(x => x.Action).HasColumnName("action").IsRequired();
            e.Property(x => x.ChangedAt).HasColumnName("changed_at").HasDefaultValueSql("now()").ValueGeneratedOnAdd();
            e.Property(x => x.ChangedByUserId).HasColumnName("changed_by_user_id");
            e.Property(x => x.VisitId).HasColumnName("visit_id");
            e.Property(x => x.Details).HasColumnName("details").HasColumnType("jsonb");
        });

        modelBuilder.Entity<Users>()
            .HasMany(x => x.Sessions)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserSession>()
            .HasMany(x => x.Events)
            .WithOne(x => x.Session)
            .HasForeignKey(x => x.SessionId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserSessionEvent>()
            .HasOne(x => x.ActorUser)
            .WithMany()
            .HasForeignKey(x => x.ActorUserId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<RefreshToken>()
            .HasOne(x => x.Session)
            .WithMany()
            .HasForeignKey(x => x.SessionId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Party>()
            .HasOne(x => x.Person)
            .WithOne(x => x.Party)
            .HasForeignKey<Person>(x => x.PartyId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Party>()
            .HasOne(x => x.Company)
            .WithOne(x => x.Party)
            .HasForeignKey<Company>(x => x.PartyId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Party>()
            .HasMany(x => x.VehicleOwnerships)
            .WithOne(x => x.Party)
            .HasForeignKey(x => x.PartyId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Party>()
            .HasMany(x => x.Visits)
            .WithOne(x => x.Party)
            .HasForeignKey(x => x.PartyId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Users>()
            .HasMany(x => x.VisitsHandled)
            .WithOne(x => x.HandledByUser)
            .HasForeignKey(x => x.HandledByUserId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<VehicleCategory>()
            .HasMany(x => x.Vehicles)
            .WithOne(x => x.Category)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Vehicle>()
            .HasMany(x => x.Visits)
            .WithOne(x => x.Vehicle)
            .HasForeignKey(x => x.VehicleId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Vehicle>()
            .HasMany(x => x.OwnershipHistory)
            .WithOne(x => x.Vehicle)
            .HasForeignKey(x => x.VehicleId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Vehicle>()
            .HasMany(x => x.Registrations)
            .WithOne(x => x.Vehicle)
            .HasForeignKey(x => x.VehicleId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Vehicle>()
            .HasMany(x => x.InsurancePolicies)
            .WithOne(x => x.Vehicle)
            .HasForeignKey(x => x.VehicleId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Vehicle>()
            .HasMany(x => x.Inspections)
            .WithOne(x => x.Vehicle)
            .HasForeignKey(x => x.VehicleId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Vehicle>()
            .HasMany(x => x.Homologations)
            .WithOne(x => x.Vehicle)
            .HasForeignKey(x => x.VehicleId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Visit>()
            .HasMany(x => x.Operations)
            .WithOne(x => x.Visit)
            .HasForeignKey(x => x.VisitId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Visit>()
            .HasMany(x => x.AuditEntries)
            .WithOne(x => x.Visit)
            .HasForeignKey(x => x.VisitId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<VisitOperation>()
            .HasMany(x => x.OwnershipChanges)
            .WithOne(x => x.VisitOperation)
            .HasForeignKey(x => x.VisitOperationId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<VisitOperation>()
            .HasMany(x => x.Registrations)
            .WithOne(x => x.VisitOperation)
            .HasForeignKey(x => x.VisitOperationId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<VisitOperation>()
            .HasMany(x => x.PlateAssignments)
            .WithOne(x => x.VisitOperation)
            .HasForeignKey(x => x.VisitOperationId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<VisitOperation>()
            .HasMany(x => x.InsurancePolicies)
            .WithOne(x => x.VisitOperation)
            .HasForeignKey(x => x.VisitOperationId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<VisitOperation>()
            .HasMany(x => x.Inspections)
            .WithOne(x => x.VisitOperation)
            .HasForeignKey(x => x.VisitOperationId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<VisitOperation>()
            .HasMany(x => x.Homologations)
            .WithOne(x => x.VisitOperation)
            .HasForeignKey(x => x.VisitOperationId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Insurer>()
            .HasMany(x => x.PolicyTemplates)
            .WithOne(x => x.Insurer)
            .HasForeignKey(x => x.InsurerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Insurer>()
            .HasMany(x => x.InsurancePolicies)
            .WithOne(x => x.Insurer)
            .HasForeignKey(x => x.InsurerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<InsurancePolicyTemplate>()
            .HasMany(x => x.InsurancePolicies)
            .WithOne(x => x.Template)
            .HasForeignKey(x => x.TemplateId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Plate>()
            .HasMany(x => x.Assignments)
            .WithOne(x => x.Plate)
            .HasForeignKey(x => x.PlateId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<VehicleRegistration>()
            .HasMany(x => x.PlateAssignments)
            .WithOne(x => x.Registration)
            .HasForeignKey(x => x.RegistrationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}


