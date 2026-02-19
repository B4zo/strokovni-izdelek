using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Customers> Customers => Set<Customers>();
        public DbSet<People> People => Set<People>();
        public DbSet<Companies> Companies => Set<Companies>();
        public DbSet<Users> Users => Set<Users>();

        public DbSet<Vehicles> Vehicles => Set<Vehicles>();
        public DbSet<VehicleOwners> VehicleOwners => Set<VehicleOwners>();

        public DbSet<Services> Services => Set<Services>();
        public DbSet<Tasks> Tasks => Set<Tasks>();
        public DbSet<ServiceTasks> ServiceTasks => Set<ServiceTasks>();

        public DbSet<VehicleRegistrations> VehicleRegistrations => Set<VehicleRegistrations>();
        public DbSet<VehicleInsurances> VehicleInsurances => Set<VehicleInsurances>();
        public DbSet<VehicleInspections> VehicleInspections => Set<VehicleInspections>();
        public DbSet<VehicleHomologations> VehicleHomologations => Set<VehicleHomologations>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("tehnicni");
            modelBuilder.HasPostgresExtension("pgcrypto");

            #region customers / people / companies

            modelBuilder.Entity<Customers>(e =>
            {
                e.ToTable("customers", tb =>
                {
                    tb.HasCheckConstraint("customers_type_check", "\"type\" IN ('person','company')");
                });

                e.HasKey(x => x.Id);

                e.Property(x => x.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("gen_random_uuid()")
                    .ValueGeneratedOnAdd();

                e.Property(x => x.Type)
                    .HasColumnName("type")
                    .IsRequired();

                e.Property(x => x.Address).HasColumnName("address");
                e.Property(x => x.Phone).HasColumnName("phone");
                e.Property(x => x.Email).HasColumnName("email");

                e.Property(x => x.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()")
                    .ValueGeneratedOnAdd();

                // Optional subtypes (enforced by app logic)
                e.HasOne(x => x.Person)
                    .WithOne(x => x.Customer)
                    .HasForeignKey<People>(x => x.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(x => x.Company)
                    .WithOne(x => x.Customer)
                    .HasForeignKey<Companies>(x => x.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasMany(x => x.VehicleOwners)
                    .WithOne(x => x.Customer)
                    .HasForeignKey(x => x.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasMany(x => x.Services)
                    .WithOne(x => x.Customer)
                    .HasForeignKey(x => x.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<People>(e =>
            {
                e.ToTable("people");
                e.HasKey(x => x.CustomerId);

                e.Property(x => x.CustomerId).HasColumnName("customer_id");

                e.Property(x => x.FullName)
                    .HasColumnName("full_name")
                    .IsRequired();

                e.Property(x => x.DateOfBirth).HasColumnName("date_of_birth");
                e.Property(x => x.TaxNumber).HasColumnName("tax_number");
                e.Property(x => x.NationalNo).HasColumnName("national_no");
            });

            modelBuilder.Entity<Companies>(e =>
            {
                e.ToTable("companies");
                e.HasKey(x => x.CustomerId);

                e.Property(x => x.CustomerId).HasColumnName("customer_id");

                e.Property(x => x.CompanyName)
                    .HasColumnName("company_name")
                    .IsRequired();

                e.Property(x => x.TaxNumber).HasColumnName("tax_number");
                e.Property(x => x.RegistrationNo).HasColumnName("registration_no");
            });

            #endregion

            #region users

            modelBuilder.Entity<Users>(e =>
            {
                e.ToTable("users", tb =>
                {
                    tb.HasCheckConstraint("users_role_check", "\"role\" IN ('employee','admin','manager','office','mechanic')");
                });

                e.HasKey(x => x.Id);

                e.Property(x => x.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("gen_random_uuid()")
                    .ValueGeneratedOnAdd();

                e.Property(x => x.Username)
                    .HasColumnName("username")
                    .IsRequired();

                e.HasIndex(x => x.Username).IsUnique();

                e.Property(x => x.PasswordHash)
                    .HasColumnName("password_hash")
                    .IsRequired();

                e.Property(x => x.DisplayName)
                    .HasColumnName("display_name")
                    .IsRequired();

                e.Property(x => x.Role)
                    .HasColumnName("role")
                    .HasDefaultValue("employee")
                    .IsRequired();

                e.Property(x => x.IsActivated)
                    .HasColumnName("is_activated")
                    .HasDefaultValue(true)
                    .IsRequired();

                e.Property(x => x.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()")
                    .ValueGeneratedOnAdd();

                e.Property(x => x.LastLoginAt).HasColumnName("last_login_at");

                e.HasMany(x => x.CreatedServices)
                    .WithOne(x => x.CreatedByUser)
                    .HasForeignKey(x => x.CreatedBy)
                    .OnDelete(DeleteBehavior.SetNull);

                e.HasMany(x => x.AssignedServices)
                    .WithOne(x => x.AssignedToUser)
                    .HasForeignKey(x => x.AssignedTo)
                    .OnDelete(DeleteBehavior.SetNull);

                e.HasMany(x => x.PerformedServiceTasks)
                    .WithOne(x => x.PerformedByUser)
                    .HasForeignKey(x => x.PerformedBy)
                    .OnDelete(DeleteBehavior.SetNull);

                e.HasMany(x => x.PerformedInspections)
                    .WithOne(x => x.PerformedByUser)
                    .HasForeignKey(x => x.PerformedBy)
                    .OnDelete(DeleteBehavior.SetNull);

                e.HasMany(x => x.HandledHomologations)
                    .WithOne(x => x.HandledByUser)
                    .HasForeignKey(x => x.HandledBy)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            #endregion

            #region vehicles / vehicle_owners

            modelBuilder.Entity<Vehicles>(e =>
            {
                e.ToTable("vehicles");

                e.HasKey(x => x.Id);

                e.Property(x => x.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("gen_random_uuid()")
                    .ValueGeneratedOnAdd();

                e.Property(x => x.Vin).HasColumnName("vin");
                e.HasIndex(x => x.Vin).IsUnique();

                e.Property(x => x.PlateNumber).HasColumnName("plate_number");
                e.HasIndex(x => x.PlateNumber).IsUnique();

                e.Property(x => x.Make).HasColumnName("make");
                e.Property(x => x.Model).HasColumnName("model");
                e.Property(x => x.Year).HasColumnName("year");
                e.Property(x => x.Category).HasColumnName("category");
                e.Property(x => x.Notes).HasColumnName("notes");
            });

            modelBuilder.Entity<VehicleOwners>(e =>
            {
                e.ToTable("vehicle_owners", tb =>
                {
                    tb.HasCheckConstraint(
                        "vehicle_owners_valid_range_check",
                        "\"valid_to\" IS NULL OR \"valid_to\" >= \"valid_from\""
                    );
                });

                e.HasKey(x => new { x.VehicleId, x.CustomerId, x.ValidFrom });

                e.Property(x => x.VehicleId).HasColumnName("vehicle_id");
                e.Property(x => x.CustomerId).HasColumnName("customer_id");

                e.Property(x => x.ValidFrom)
                    .HasColumnName("valid_from")
                    .HasDefaultValueSql("CURRENT_DATE");

                e.Property(x => x.ValidTo).HasColumnName("valid_to");
            });

            #endregion

            #region services / tasks / service_tasks

            modelBuilder.Entity<Services>(e =>
            {
                e.ToTable("services", tb =>
                {
                    tb.HasCheckConstraint(
                        "services_status_check",
                        "\"status\" IN ('open','waiting_docs','in_progress','completed','cancelled')"
                    );
                });

                e.HasKey(x => x.Id);

                e.Property(x => x.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("gen_random_uuid()")
                    .ValueGeneratedOnAdd();

                e.Property(x => x.CustomerId).HasColumnName("customer_id").IsRequired();
                e.Property(x => x.VehicleId).HasColumnName("vehicle_id").IsRequired();

                e.Property(x => x.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()")
                    .ValueGeneratedOnAdd();

                e.Property(x => x.Status)
                    .HasColumnName("status")
                    .HasDefaultValue("open")
                    .IsRequired();

                e.Property(x => x.CreatedBy).HasColumnName("created_by");
                e.Property(x => x.AssignedTo).HasColumnName("assigned_to");
                e.Property(x => x.Notes).HasColumnName("notes");
            });

            modelBuilder.Entity<Tasks>(e =>
            {
                e.ToTable("tasks");

                e.HasKey(x => x.Id);

                e.Property(x => x.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("gen_random_uuid()")
                    .ValueGeneratedOnAdd();

                e.Property(x => x.Code)
                    .HasColumnName("code")
                    .IsRequired();

                e.HasIndex(x => x.Code).IsUnique();

                e.Property(x => x.Name)
                    .HasColumnName("name")
                    .IsRequired();
            });

            modelBuilder.Entity<ServiceTasks>(e =>
            {
                e.ToTable("service_tasks", tb =>
                {
                    tb.HasCheckConstraint("service_tasks_status_check", "\"status\" IN ('pending','done','failed')");
                });

                e.HasKey(x => new { x.ServiceId, x.TaskId });

                e.Property(x => x.ServiceId).HasColumnName("service_id");
                e.Property(x => x.TaskId).HasColumnName("task_id");

                e.Property(x => x.Status)
                    .HasColumnName("status")
                    .HasDefaultValue("pending")
                    .IsRequired();

                e.Property(x => x.PerformedBy).HasColumnName("performed_by");
                e.Property(x => x.PerformedAt).HasColumnName("performed_at");
                e.Property(x => x.Notes).HasColumnName("notes");
            });

            #endregion

            #region vehicle_registrations / vehicle_insurances / vehicle_inspections / vehicle_homologations

            modelBuilder.Entity<VehicleRegistrations>(e =>
            {
                e.ToTable("vehicle_registrations", tb =>
                {
                    tb.HasCheckConstraint("vehicle_registrations_valid_range_check", "\"valid_to\" >= \"valid_from\"");
                });

                e.HasKey(x => x.Id);

                e.Property(x => x.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("gen_random_uuid()")
                    .ValueGeneratedOnAdd();

                e.Property(x => x.VehicleId).HasColumnName("vehicle_id").IsRequired();
                e.Property(x => x.ServiceId).HasColumnName("service_id");

                e.Property(x => x.ValidFrom).HasColumnName("valid_from").IsRequired();
                e.Property(x => x.ValidTo).HasColumnName("valid_to").IsRequired();

                e.Property(x => x.PlateNumber).HasColumnName("plate_number");
                e.Property(x => x.Notes).HasColumnName("notes");
            });

            modelBuilder.Entity<VehicleInsurances>(e =>
            {
                e.ToTable("vehicle_insurances", tb =>
                {
                    tb.HasCheckConstraint("vehicle_insurances_valid_range_check", "\"valid_to\" >= \"valid_from\"");
                });

                e.HasKey(x => x.Id);

                e.Property(x => x.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("gen_random_uuid()")
                    .ValueGeneratedOnAdd();

                e.Property(x => x.VehicleId).HasColumnName("vehicle_id").IsRequired();
                e.Property(x => x.ServiceId).HasColumnName("service_id");

                e.Property(x => x.InsurerName).HasColumnName("insurer_name").IsRequired();
                e.Property(x => x.PolicyNumber).HasColumnName("policy_number");

                e.Property(x => x.ValidFrom).HasColumnName("valid_from").IsRequired();
                e.Property(x => x.ValidTo).HasColumnName("valid_to").IsRequired();

                e.Property(x => x.CoverageType).HasColumnName("coverage_type");
                e.Property(x => x.Notes).HasColumnName("notes");
            });

            modelBuilder.Entity<VehicleInspections>(e =>
            {
                e.ToTable("vehicle_inspections", tb =>
                {
                    tb.HasCheckConstraint(
                        "vehicle_inspections_result_check",
                        "\"result\" IN ('pass','fail','conditional')"
                    );
                });

                e.HasKey(x => x.Id);

                e.Property(x => x.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("gen_random_uuid()")
                    .ValueGeneratedOnAdd();

                e.Property(x => x.VehicleId).HasColumnName("vehicle_id").IsRequired();
                e.Property(x => x.ServiceId).HasColumnName("service_id");

                e.Property(x => x.PerformedBy).HasColumnName("performed_by");
                e.Property(x => x.PerformedAt).HasColumnName("performed_at").IsRequired();

                e.Property(x => x.Result).HasColumnName("result").IsRequired();
                e.Property(x => x.ValidUntil).HasColumnName("valid_until");

                e.Property(x => x.OdometerKm).HasColumnName("odometer_km");
                e.Property(x => x.Notes).HasColumnName("notes");
            });

            modelBuilder.Entity<VehicleHomologations>(e =>
            {
                e.ToTable("vehicle_homologations", tb =>
                {
                    tb.HasCheckConstraint(
                        "vehicle_homologations_kind_check",
                        "\"kind\" IN ('coc','import','modification','individual_approval','data_correction')"
                    );
                });

                e.HasKey(x => x.Id);

                e.Property(x => x.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("gen_random_uuid()")
                    .ValueGeneratedOnAdd();

                e.Property(x => x.VehicleId).HasColumnName("vehicle_id").IsRequired();
                e.Property(x => x.ServiceId).HasColumnName("service_id");

                e.Property(x => x.HandledBy).HasColumnName("handled_by");

                e.Property(x => x.Kind).HasColumnName("kind").IsRequired();

                e.Property(x => x.DocumentNo).HasColumnName("document_no");
                e.Property(x => x.IssuedAt).HasColumnName("issued_at");
                e.Property(x => x.ValidUntil).HasColumnName("valid_until");
                e.Property(x => x.Notes).HasColumnName("notes");
            });

            #endregion
        }
    }
}
