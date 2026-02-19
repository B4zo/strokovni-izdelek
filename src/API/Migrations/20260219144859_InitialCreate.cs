using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "tehnicni");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:pgcrypto", ",,");

            migrationBuilder.CreateTable(
                name: "customers",
                schema: "tehnicni",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    type = table.Column<string>(type: "text", nullable: false),
                    address = table.Column<string>(type: "text", nullable: true),
                    phone = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.id);
                    table.CheckConstraint("customers_type_check", "\"type\" IN ('person','company')");
                });

            migrationBuilder.CreateTable(
                name: "tasks",
                schema: "tehnicni",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    code = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tasks", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "tehnicni",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    username = table.Column<string>(type: "text", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: false),
                    display_name = table.Column<string>(type: "text", nullable: false),
                    role = table.Column<string>(type: "text", nullable: false, defaultValue: "employee"),
                    is_activated = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    last_login_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                    table.CheckConstraint("users_role_check", "\"role\" IN ('employee','admin','manager','office','mechanic')");
                });

            migrationBuilder.CreateTable(
                name: "vehicles",
                schema: "tehnicni",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    vin = table.Column<string>(type: "text", nullable: true),
                    plate_number = table.Column<string>(type: "text", nullable: true),
                    make = table.Column<string>(type: "text", nullable: true),
                    model = table.Column<string>(type: "text", nullable: true),
                    year = table.Column<int>(type: "integer", nullable: true),
                    category = table.Column<string>(type: "text", nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "companies",
                schema: "tehnicni",
                columns: table => new
                {
                    customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    company_name = table.Column<string>(type: "text", nullable: false),
                    tax_number = table.Column<string>(type: "text", nullable: true),
                    registration_no = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_companies", x => x.customer_id);
                    table.ForeignKey(
                        name: "FK_companies_customers_customer_id",
                        column: x => x.customer_id,
                        principalSchema: "tehnicni",
                        principalTable: "customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "people",
                schema: "tehnicni",
                columns: table => new
                {
                    customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    full_name = table.Column<string>(type: "text", nullable: false),
                    date_of_birth = table.Column<DateOnly>(type: "date", nullable: true),
                    tax_number = table.Column<string>(type: "text", nullable: true),
                    national_no = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_people", x => x.customer_id);
                    table.ForeignKey(
                        name: "FK_people_customers_customer_id",
                        column: x => x.customer_id,
                        principalSchema: "tehnicni",
                        principalTable: "customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "services",
                schema: "tehnicni",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    vehicle_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    status = table.Column<string>(type: "text", nullable: false, defaultValue: "open"),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    assigned_to = table.Column<Guid>(type: "uuid", nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_services", x => x.id);
                    table.CheckConstraint("services_status_check", "\"status\" IN ('open','waiting_docs','in_progress','completed','cancelled')");
                    table.ForeignKey(
                        name: "FK_services_customers_customer_id",
                        column: x => x.customer_id,
                        principalSchema: "tehnicni",
                        principalTable: "customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_services_users_assigned_to",
                        column: x => x.assigned_to,
                        principalSchema: "tehnicni",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_services_users_created_by",
                        column: x => x.created_by,
                        principalSchema: "tehnicni",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_services_vehicles_vehicle_id",
                        column: x => x.vehicle_id,
                        principalSchema: "tehnicni",
                        principalTable: "vehicles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vehicle_owners",
                schema: "tehnicni",
                columns: table => new
                {
                    vehicle_id = table.Column<Guid>(type: "uuid", nullable: false),
                    customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    valid_from = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "CURRENT_DATE"),
                    valid_to = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicle_owners", x => new { x.vehicle_id, x.customer_id, x.valid_from });
                    table.CheckConstraint("vehicle_owners_valid_range_check", "\"valid_to\" IS NULL OR \"valid_to\" >= \"valid_from\"");
                    table.ForeignKey(
                        name: "FK_vehicle_owners_customers_customer_id",
                        column: x => x.customer_id,
                        principalSchema: "tehnicni",
                        principalTable: "customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_vehicle_owners_vehicles_vehicle_id",
                        column: x => x.vehicle_id,
                        principalSchema: "tehnicni",
                        principalTable: "vehicles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "service_tasks",
                schema: "tehnicni",
                columns: table => new
                {
                    service_id = table.Column<Guid>(type: "uuid", nullable: false),
                    task_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false, defaultValue: "pending"),
                    performed_by = table.Column<Guid>(type: "uuid", nullable: true),
                    performed_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_service_tasks", x => new { x.service_id, x.task_id });
                    table.CheckConstraint("service_tasks_status_check", "\"status\" IN ('pending','done','failed')");
                    table.ForeignKey(
                        name: "FK_service_tasks_services_service_id",
                        column: x => x.service_id,
                        principalSchema: "tehnicni",
                        principalTable: "services",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_service_tasks_tasks_task_id",
                        column: x => x.task_id,
                        principalSchema: "tehnicni",
                        principalTable: "tasks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_service_tasks_users_performed_by",
                        column: x => x.performed_by,
                        principalSchema: "tehnicni",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "vehicle_homologations",
                schema: "tehnicni",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    vehicle_id = table.Column<Guid>(type: "uuid", nullable: false),
                    service_id = table.Column<Guid>(type: "uuid", nullable: true),
                    handled_by = table.Column<Guid>(type: "uuid", nullable: true),
                    kind = table.Column<string>(type: "text", nullable: false),
                    document_no = table.Column<string>(type: "text", nullable: true),
                    issued_at = table.Column<DateOnly>(type: "date", nullable: true),
                    valid_until = table.Column<DateOnly>(type: "date", nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicle_homologations", x => x.id);
                    table.CheckConstraint("vehicle_homologations_kind_check", "\"kind\" IN ('coc','import','modification','individual_approval','data_correction')");
                    table.ForeignKey(
                        name: "FK_vehicle_homologations_services_service_id",
                        column: x => x.service_id,
                        principalSchema: "tehnicni",
                        principalTable: "services",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_vehicle_homologations_users_handled_by",
                        column: x => x.handled_by,
                        principalSchema: "tehnicni",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_vehicle_homologations_vehicles_vehicle_id",
                        column: x => x.vehicle_id,
                        principalSchema: "tehnicni",
                        principalTable: "vehicles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vehicle_inspections",
                schema: "tehnicni",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    vehicle_id = table.Column<Guid>(type: "uuid", nullable: false),
                    service_id = table.Column<Guid>(type: "uuid", nullable: true),
                    performed_by = table.Column<Guid>(type: "uuid", nullable: true),
                    performed_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    result = table.Column<string>(type: "text", nullable: false),
                    valid_until = table.Column<DateOnly>(type: "date", nullable: true),
                    odometer_km = table.Column<int>(type: "integer", nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicle_inspections", x => x.id);
                    table.CheckConstraint("vehicle_inspections_result_check", "\"result\" IN ('pass','fail','conditional')");
                    table.ForeignKey(
                        name: "FK_vehicle_inspections_services_service_id",
                        column: x => x.service_id,
                        principalSchema: "tehnicni",
                        principalTable: "services",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_vehicle_inspections_users_performed_by",
                        column: x => x.performed_by,
                        principalSchema: "tehnicni",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_vehicle_inspections_vehicles_vehicle_id",
                        column: x => x.vehicle_id,
                        principalSchema: "tehnicni",
                        principalTable: "vehicles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vehicle_insurances",
                schema: "tehnicni",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    vehicle_id = table.Column<Guid>(type: "uuid", nullable: false),
                    service_id = table.Column<Guid>(type: "uuid", nullable: true),
                    insurer_name = table.Column<string>(type: "text", nullable: false),
                    policy_number = table.Column<string>(type: "text", nullable: true),
                    valid_from = table.Column<DateOnly>(type: "date", nullable: false),
                    valid_to = table.Column<DateOnly>(type: "date", nullable: false),
                    coverage_type = table.Column<string>(type: "text", nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicle_insurances", x => x.id);
                    table.CheckConstraint("vehicle_insurances_valid_range_check", "\"valid_to\" >= \"valid_from\"");
                    table.ForeignKey(
                        name: "FK_vehicle_insurances_services_service_id",
                        column: x => x.service_id,
                        principalSchema: "tehnicni",
                        principalTable: "services",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_vehicle_insurances_vehicles_vehicle_id",
                        column: x => x.vehicle_id,
                        principalSchema: "tehnicni",
                        principalTable: "vehicles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vehicle_registrations",
                schema: "tehnicni",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    vehicle_id = table.Column<Guid>(type: "uuid", nullable: false),
                    service_id = table.Column<Guid>(type: "uuid", nullable: true),
                    valid_from = table.Column<DateOnly>(type: "date", nullable: false),
                    valid_to = table.Column<DateOnly>(type: "date", nullable: false),
                    plate_number = table.Column<string>(type: "text", nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicle_registrations", x => x.id);
                    table.CheckConstraint("vehicle_registrations_valid_range_check", "\"valid_to\" >= \"valid_from\"");
                    table.ForeignKey(
                        name: "FK_vehicle_registrations_services_service_id",
                        column: x => x.service_id,
                        principalSchema: "tehnicni",
                        principalTable: "services",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_vehicle_registrations_vehicles_vehicle_id",
                        column: x => x.vehicle_id,
                        principalSchema: "tehnicni",
                        principalTable: "vehicles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_service_tasks_performed_by",
                schema: "tehnicni",
                table: "service_tasks",
                column: "performed_by");

            migrationBuilder.CreateIndex(
                name: "IX_service_tasks_task_id",
                schema: "tehnicni",
                table: "service_tasks",
                column: "task_id");

            migrationBuilder.CreateIndex(
                name: "IX_services_assigned_to",
                schema: "tehnicni",
                table: "services",
                column: "assigned_to");

            migrationBuilder.CreateIndex(
                name: "IX_services_created_by",
                schema: "tehnicni",
                table: "services",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_services_customer_id",
                schema: "tehnicni",
                table: "services",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_services_vehicle_id",
                schema: "tehnicni",
                table: "services",
                column: "vehicle_id");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_code",
                schema: "tehnicni",
                table: "tasks",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_username",
                schema: "tehnicni",
                table: "users",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_homologations_handled_by",
                schema: "tehnicni",
                table: "vehicle_homologations",
                column: "handled_by");

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_homologations_service_id",
                schema: "tehnicni",
                table: "vehicle_homologations",
                column: "service_id");

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_homologations_vehicle_id",
                schema: "tehnicni",
                table: "vehicle_homologations",
                column: "vehicle_id");

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_inspections_performed_by",
                schema: "tehnicni",
                table: "vehicle_inspections",
                column: "performed_by");

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_inspections_service_id",
                schema: "tehnicni",
                table: "vehicle_inspections",
                column: "service_id");

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_inspections_vehicle_id",
                schema: "tehnicni",
                table: "vehicle_inspections",
                column: "vehicle_id");

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_insurances_service_id",
                schema: "tehnicni",
                table: "vehicle_insurances",
                column: "service_id");

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_insurances_vehicle_id",
                schema: "tehnicni",
                table: "vehicle_insurances",
                column: "vehicle_id");

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_owners_customer_id",
                schema: "tehnicni",
                table: "vehicle_owners",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_registrations_service_id",
                schema: "tehnicni",
                table: "vehicle_registrations",
                column: "service_id");

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_registrations_vehicle_id",
                schema: "tehnicni",
                table: "vehicle_registrations",
                column: "vehicle_id");

            migrationBuilder.CreateIndex(
                name: "IX_vehicles_plate_number",
                schema: "tehnicni",
                table: "vehicles",
                column: "plate_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_vehicles_vin",
                schema: "tehnicni",
                table: "vehicles",
                column: "vin",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "companies",
                schema: "tehnicni");

            migrationBuilder.DropTable(
                name: "people",
                schema: "tehnicni");

            migrationBuilder.DropTable(
                name: "service_tasks",
                schema: "tehnicni");

            migrationBuilder.DropTable(
                name: "vehicle_homologations",
                schema: "tehnicni");

            migrationBuilder.DropTable(
                name: "vehicle_inspections",
                schema: "tehnicni");

            migrationBuilder.DropTable(
                name: "vehicle_insurances",
                schema: "tehnicni");

            migrationBuilder.DropTable(
                name: "vehicle_owners",
                schema: "tehnicni");

            migrationBuilder.DropTable(
                name: "vehicle_registrations",
                schema: "tehnicni");

            migrationBuilder.DropTable(
                name: "tasks",
                schema: "tehnicni");

            migrationBuilder.DropTable(
                name: "services",
                schema: "tehnicni");

            migrationBuilder.DropTable(
                name: "customers",
                schema: "tehnicni");

            migrationBuilder.DropTable(
                name: "users",
                schema: "tehnicni");

            migrationBuilder.DropTable(
                name: "vehicles",
                schema: "tehnicni");
        }
    }
}
