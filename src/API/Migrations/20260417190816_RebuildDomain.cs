using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class RebuildDomain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vehicle_registrations_services_service_id",
                schema: "tehnicni",
                table: "vehicle_registrations");

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
                name: "tasks",
                schema: "tehnicni");

            migrationBuilder.DropTable(
                name: "services",
                schema: "tehnicni");

            migrationBuilder.DropIndex(
                name: "IX_vehicles_plate_number",
                schema: "tehnicni",
                table: "vehicles");

            migrationBuilder.DropIndex(
                name: "IX_vehicle_registrations_service_id",
                schema: "tehnicni",
                table: "vehicle_registrations");

            migrationBuilder.DropCheckConstraint(
                name: "vehicle_registrations_valid_range_check",
                schema: "tehnicni",
                table: "vehicle_registrations");

            migrationBuilder.DropColumn(
                name: "category",
                schema: "tehnicni",
                table: "vehicles");

            migrationBuilder.DropColumn(
                name: "plate_number",
                schema: "tehnicni",
                table: "vehicles");

            migrationBuilder.DropColumn(
                name: "service_id",
                schema: "tehnicni",
                table: "vehicle_registrations");

            migrationBuilder.RenameColumn(
                name: "plate_number",
                schema: "tehnicni",
                table: "vehicle_registrations",
                newName: "registration_no");

            migrationBuilder.AlterColumn<string>(
                name: "vin",
                schema: "tehnicni",
                table: "vehicles",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "category_id",
                schema: "tehnicni",
                table: "vehicles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "valid_to",
                schema: "tehnicni",
                table: "vehicle_registrations",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<Guid>(
                name: "customer_id",
                schema: "tehnicni",
                table: "vehicle_registrations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "visit_operation_id",
                schema: "tehnicni",
                table: "vehicle_registrations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "insurers",
                schema: "tehnicni",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    code = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_insurers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "plates",
                schema: "tehnicni",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    plate_number = table.Column<string>(type: "text", nullable: false),
                    region_code = table.Column<string>(type: "text", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_plates", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vehicle_categories",
                schema: "tehnicni",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    code = table.Column<string>(type: "text", nullable: false),
                    label = table.Column<string>(type: "text", nullable: false),
                    category_group = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicle_categories", x => x.id);
                    table.CheckConstraint("vehicle_categories_code_check", "\"code\" IN ('L1e','L2e','L3e','L4e','L5e','L6e','L7e','M1','M2','M3','N1','N2','N3','O1','O2','O3','O4','T1','T2','T3','T4','T5','C1','C2','C3','C4')");
                });

            migrationBuilder.CreateTable(
                name: "visits",
                schema: "tehnicni",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    vehicle_id = table.Column<Guid>(type: "uuid", nullable: true),
                    visited_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    handled_by_user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_visits", x => x.id);
                    table.ForeignKey(
                        name: "FK_visits_customers_customer_id",
                        column: x => x.customer_id,
                        principalSchema: "tehnicni",
                        principalTable: "customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_visits_users_handled_by_user_id",
                        column: x => x.handled_by_user_id,
                        principalSchema: "tehnicni",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_visits_vehicles_vehicle_id",
                        column: x => x.vehicle_id,
                        principalSchema: "tehnicni",
                        principalTable: "vehicles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "insurance_policy_templates",
                schema: "tehnicni",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    insurer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    scope = table.Column<string>(type: "text", nullable: true),
                    active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_insurance_policy_templates", x => x.id);
                    table.ForeignKey(
                        name: "FK_insurance_policy_templates_insurers_insurer_id",
                        column: x => x.insurer_id,
                        principalSchema: "tehnicni",
                        principalTable: "insurers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "audit_log",
                schema: "tehnicni",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    entity_name = table.Column<string>(type: "text", nullable: false),
                    entity_id = table.Column<Guid>(type: "uuid", nullable: false),
                    action = table.Column<string>(type: "text", nullable: false),
                    changed_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    changed_by_user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    visit_id = table.Column<Guid>(type: "uuid", nullable: true),
                    details = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_audit_log", x => x.id);
                    table.ForeignKey(
                        name: "FK_audit_log_users_changed_by_user_id",
                        column: x => x.changed_by_user_id,
                        principalSchema: "tehnicni",
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_audit_log_visits_visit_id",
                        column: x => x.visit_id,
                        principalSchema: "tehnicni",
                        principalTable: "visits",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "visit_operations",
                schema: "tehnicni",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    visit_id = table.Column<Guid>(type: "uuid", nullable: false),
                    op_type = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_visit_operations", x => x.id);
                    table.CheckConstraint("visit_operations_type_check", "\"op_type\" IN ('ownership_change','registration_issue','registration_extension','plate_assignment','insurance_issue','inspection_finish','homologation','other')");
                    table.ForeignKey(
                        name: "FK_visit_operations_visits_visit_id",
                        column: x => x.visit_id,
                        principalSchema: "tehnicni",
                        principalTable: "visits",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "homologations",
                schema: "tehnicni",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    visit_operation_id = table.Column<Guid>(type: "uuid", nullable: false),
                    vehicle_id = table.Column<Guid>(type: "uuid", nullable: false),
                    handled_by_user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    kind = table.Column<string>(type: "text", nullable: false),
                    document_no = table.Column<string>(type: "text", nullable: true),
                    issued_at = table.Column<DateOnly>(type: "date", nullable: true),
                    valid_until = table.Column<DateOnly>(type: "date", nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_homologations", x => x.id);
                    table.CheckConstraint("homologations_kind_check", "\"kind\" IN ('coc','import','modification','individual_approval','data_correction')");
                    table.ForeignKey(
                        name: "FK_homologations_users_handled_by_user_id",
                        column: x => x.handled_by_user_id,
                        principalSchema: "tehnicni",
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_homologations_vehicles_vehicle_id",
                        column: x => x.vehicle_id,
                        principalSchema: "tehnicni",
                        principalTable: "vehicles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_homologations_visit_operations_visit_operation_id",
                        column: x => x.visit_operation_id,
                        principalSchema: "tehnicni",
                        principalTable: "visit_operations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "inspections",
                schema: "tehnicni",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    visit_operation_id = table.Column<Guid>(type: "uuid", nullable: false),
                    vehicle_id = table.Column<Guid>(type: "uuid", nullable: false),
                    performed_by_user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    performed_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    result = table.Column<string>(type: "text", nullable: false),
                    finished = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    valid_until = table.Column<DateOnly>(type: "date", nullable: true),
                    odometer_km = table.Column<int>(type: "integer", nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inspections", x => x.id);
                    table.CheckConstraint("inspections_result_check", "\"result\" IN ('pending','passed','failed','conditional')");
                    table.ForeignKey(
                        name: "FK_inspections_users_performed_by_user_id",
                        column: x => x.performed_by_user_id,
                        principalSchema: "tehnicni",
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_inspections_vehicles_vehicle_id",
                        column: x => x.vehicle_id,
                        principalSchema: "tehnicni",
                        principalTable: "vehicles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inspections_visit_operations_visit_operation_id",
                        column: x => x.visit_operation_id,
                        principalSchema: "tehnicni",
                        principalTable: "visit_operations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "insurance_policies",
                schema: "tehnicni",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    visit_operation_id = table.Column<Guid>(type: "uuid", nullable: false),
                    vehicle_id = table.Column<Guid>(type: "uuid", nullable: false),
                    customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    insurer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    template_id = table.Column<Guid>(type: "uuid", nullable: false),
                    policy_no = table.Column<string>(type: "text", nullable: true),
                    valid_from = table.Column<DateOnly>(type: "date", nullable: false),
                    valid_to = table.Column<DateOnly>(type: "date", nullable: true),
                    premium_amount = table.Column<decimal>(type: "numeric(12,2)", nullable: true),
                    currency = table.Column<string>(type: "text", nullable: false, defaultValue: "EUR"),
                    notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_insurance_policies", x => x.id);
                    table.CheckConstraint("insurance_policies_valid_range_check", "\"valid_to\" IS NULL OR \"valid_to\" >= \"valid_from\"");
                    table.ForeignKey(
                        name: "FK_insurance_policies_customers_customer_id",
                        column: x => x.customer_id,
                        principalSchema: "tehnicni",
                        principalTable: "customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_insurance_policies_insurance_policy_templates_template_id",
                        column: x => x.template_id,
                        principalSchema: "tehnicni",
                        principalTable: "insurance_policy_templates",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_insurance_policies_insurers_insurer_id",
                        column: x => x.insurer_id,
                        principalSchema: "tehnicni",
                        principalTable: "insurers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_insurance_policies_vehicles_vehicle_id",
                        column: x => x.vehicle_id,
                        principalSchema: "tehnicni",
                        principalTable: "vehicles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_insurance_policies_visit_operations_visit_operation_id",
                        column: x => x.visit_operation_id,
                        principalSchema: "tehnicni",
                        principalTable: "visit_operations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "plate_assignments",
                schema: "tehnicni",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    visit_operation_id = table.Column<Guid>(type: "uuid", nullable: false),
                    plate_id = table.Column<Guid>(type: "uuid", nullable: false),
                    registration_id = table.Column<Guid>(type: "uuid", nullable: false),
                    valid_from = table.Column<DateOnly>(type: "date", nullable: false),
                    valid_to = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_plate_assignments", x => x.id);
                    table.CheckConstraint("plate_assignments_valid_range_check", "\"valid_to\" IS NULL OR \"valid_to\" >= \"valid_from\"");
                    table.ForeignKey(
                        name: "FK_plate_assignments_plates_plate_id",
                        column: x => x.plate_id,
                        principalSchema: "tehnicni",
                        principalTable: "plates",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_plate_assignments_vehicle_registrations_registration_id",
                        column: x => x.registration_id,
                        principalSchema: "tehnicni",
                        principalTable: "vehicle_registrations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_plate_assignments_visit_operations_visit_operation_id",
                        column: x => x.visit_operation_id,
                        principalSchema: "tehnicni",
                        principalTable: "visit_operations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vehicle_ownerships",
                schema: "tehnicni",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    visit_operation_id = table.Column<Guid>(type: "uuid", nullable: false),
                    vehicle_id = table.Column<Guid>(type: "uuid", nullable: false),
                    customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    valid_from = table.Column<DateOnly>(type: "date", nullable: false),
                    valid_to = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicle_ownerships", x => x.id);
                    table.CheckConstraint("vehicle_ownerships_valid_range_check", "\"valid_to\" IS NULL OR \"valid_to\" >= \"valid_from\"");
                    table.ForeignKey(
                        name: "FK_vehicle_ownerships_customers_customer_id",
                        column: x => x.customer_id,
                        principalSchema: "tehnicni",
                        principalTable: "customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_vehicle_ownerships_vehicles_vehicle_id",
                        column: x => x.vehicle_id,
                        principalSchema: "tehnicni",
                        principalTable: "vehicles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_vehicle_ownerships_visit_operations_visit_operation_id",
                        column: x => x.visit_operation_id,
                        principalSchema: "tehnicni",
                        principalTable: "visit_operations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_vehicles_category_id",
                schema: "tehnicni",
                table: "vehicles",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_registrations_customer_id",
                schema: "tehnicni",
                table: "vehicle_registrations",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_registrations_visit_operation_id",
                schema: "tehnicni",
                table: "vehicle_registrations",
                column: "visit_operation_id");

            migrationBuilder.AddCheckConstraint(
                name: "vehicle_registrations_valid_range_check",
                schema: "tehnicni",
                table: "vehicle_registrations",
                sql: "\"valid_to\" IS NULL OR \"valid_to\" >= \"valid_from\"");

            migrationBuilder.CreateIndex(
                name: "IX_audit_log_changed_by_user_id",
                schema: "tehnicni",
                table: "audit_log",
                column: "changed_by_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_audit_log_visit_id",
                schema: "tehnicni",
                table: "audit_log",
                column: "visit_id");

            migrationBuilder.CreateIndex(
                name: "IX_homologations_handled_by_user_id",
                schema: "tehnicni",
                table: "homologations",
                column: "handled_by_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_homologations_vehicle_id",
                schema: "tehnicni",
                table: "homologations",
                column: "vehicle_id");

            migrationBuilder.CreateIndex(
                name: "IX_homologations_visit_operation_id",
                schema: "tehnicni",
                table: "homologations",
                column: "visit_operation_id");

            migrationBuilder.CreateIndex(
                name: "IX_inspections_performed_by_user_id",
                schema: "tehnicni",
                table: "inspections",
                column: "performed_by_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_inspections_vehicle_id",
                schema: "tehnicni",
                table: "inspections",
                column: "vehicle_id");

            migrationBuilder.CreateIndex(
                name: "IX_inspections_visit_operation_id",
                schema: "tehnicni",
                table: "inspections",
                column: "visit_operation_id");

            migrationBuilder.CreateIndex(
                name: "IX_insurance_policies_customer_id",
                schema: "tehnicni",
                table: "insurance_policies",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_insurance_policies_insurer_id",
                schema: "tehnicni",
                table: "insurance_policies",
                column: "insurer_id");

            migrationBuilder.CreateIndex(
                name: "IX_insurance_policies_template_id",
                schema: "tehnicni",
                table: "insurance_policies",
                column: "template_id");

            migrationBuilder.CreateIndex(
                name: "IX_insurance_policies_vehicle_id",
                schema: "tehnicni",
                table: "insurance_policies",
                column: "vehicle_id");

            migrationBuilder.CreateIndex(
                name: "IX_insurance_policies_visit_operation_id",
                schema: "tehnicni",
                table: "insurance_policies",
                column: "visit_operation_id");

            migrationBuilder.CreateIndex(
                name: "IX_insurance_policy_templates_insurer_id_code",
                schema: "tehnicni",
                table: "insurance_policy_templates",
                columns: new[] { "insurer_id", "code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_insurers_code",
                schema: "tehnicni",
                table: "insurers",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_plate_assignments_plate_id",
                schema: "tehnicni",
                table: "plate_assignments",
                column: "plate_id");

            migrationBuilder.CreateIndex(
                name: "IX_plate_assignments_registration_id",
                schema: "tehnicni",
                table: "plate_assignments",
                column: "registration_id");

            migrationBuilder.CreateIndex(
                name: "IX_plate_assignments_visit_operation_id",
                schema: "tehnicni",
                table: "plate_assignments",
                column: "visit_operation_id");

            migrationBuilder.CreateIndex(
                name: "IX_plates_plate_number",
                schema: "tehnicni",
                table: "plates",
                column: "plate_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_categories_code",
                schema: "tehnicni",
                table: "vehicle_categories",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_ownerships_customer_id",
                schema: "tehnicni",
                table: "vehicle_ownerships",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_ownerships_vehicle_id",
                schema: "tehnicni",
                table: "vehicle_ownerships",
                column: "vehicle_id");

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_ownerships_visit_operation_id",
                schema: "tehnicni",
                table: "vehicle_ownerships",
                column: "visit_operation_id");

            migrationBuilder.CreateIndex(
                name: "IX_visit_operations_visit_id",
                schema: "tehnicni",
                table: "visit_operations",
                column: "visit_id");

            migrationBuilder.CreateIndex(
                name: "IX_visits_customer_id",
                schema: "tehnicni",
                table: "visits",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_visits_handled_by_user_id",
                schema: "tehnicni",
                table: "visits",
                column: "handled_by_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_visits_vehicle_id",
                schema: "tehnicni",
                table: "visits",
                column: "vehicle_id");

            migrationBuilder.AddForeignKey(
                name: "FK_vehicle_registrations_customers_customer_id",
                schema: "tehnicni",
                table: "vehicle_registrations",
                column: "customer_id",
                principalSchema: "tehnicni",
                principalTable: "customers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_vehicle_registrations_visit_operations_visit_operation_id",
                schema: "tehnicni",
                table: "vehicle_registrations",
                column: "visit_operation_id",
                principalSchema: "tehnicni",
                principalTable: "visit_operations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_vehicles_vehicle_categories_category_id",
                schema: "tehnicni",
                table: "vehicles",
                column: "category_id",
                principalSchema: "tehnicni",
                principalTable: "vehicle_categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vehicle_registrations_customers_customer_id",
                schema: "tehnicni",
                table: "vehicle_registrations");

            migrationBuilder.DropForeignKey(
                name: "FK_vehicle_registrations_visit_operations_visit_operation_id",
                schema: "tehnicni",
                table: "vehicle_registrations");

            migrationBuilder.DropForeignKey(
                name: "FK_vehicles_vehicle_categories_category_id",
                schema: "tehnicni",
                table: "vehicles");

            migrationBuilder.DropTable(
                name: "audit_log",
                schema: "tehnicni");

            migrationBuilder.DropTable(
                name: "homologations",
                schema: "tehnicni");

            migrationBuilder.DropTable(
                name: "inspections",
                schema: "tehnicni");

            migrationBuilder.DropTable(
                name: "insurance_policies",
                schema: "tehnicni");

            migrationBuilder.DropTable(
                name: "plate_assignments",
                schema: "tehnicni");

            migrationBuilder.DropTable(
                name: "vehicle_categories",
                schema: "tehnicni");

            migrationBuilder.DropTable(
                name: "vehicle_ownerships",
                schema: "tehnicni");

            migrationBuilder.DropTable(
                name: "insurance_policy_templates",
                schema: "tehnicni");

            migrationBuilder.DropTable(
                name: "plates",
                schema: "tehnicni");

            migrationBuilder.DropTable(
                name: "visit_operations",
                schema: "tehnicni");

            migrationBuilder.DropTable(
                name: "insurers",
                schema: "tehnicni");

            migrationBuilder.DropTable(
                name: "visits",
                schema: "tehnicni");

            migrationBuilder.DropIndex(
                name: "IX_vehicles_category_id",
                schema: "tehnicni",
                table: "vehicles");

            migrationBuilder.DropIndex(
                name: "IX_vehicle_registrations_customer_id",
                schema: "tehnicni",
                table: "vehicle_registrations");

            migrationBuilder.DropIndex(
                name: "IX_vehicle_registrations_visit_operation_id",
                schema: "tehnicni",
                table: "vehicle_registrations");

            migrationBuilder.DropCheckConstraint(
                name: "vehicle_registrations_valid_range_check",
                schema: "tehnicni",
                table: "vehicle_registrations");

            migrationBuilder.DropColumn(
                name: "category_id",
                schema: "tehnicni",
                table: "vehicles");

            migrationBuilder.DropColumn(
                name: "customer_id",
                schema: "tehnicni",
                table: "vehicle_registrations");

            migrationBuilder.DropColumn(
                name: "visit_operation_id",
                schema: "tehnicni",
                table: "vehicle_registrations");

            migrationBuilder.RenameColumn(
                name: "registration_no",
                schema: "tehnicni",
                table: "vehicle_registrations",
                newName: "plate_number");

            migrationBuilder.AlterColumn<string>(
                name: "vin",
                schema: "tehnicni",
                table: "vehicles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "category",
                schema: "tehnicni",
                table: "vehicles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "plate_number",
                schema: "tehnicni",
                table: "vehicles",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "valid_to",
                schema: "tehnicni",
                table: "vehicle_registrations",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "service_id",
                schema: "tehnicni",
                table: "vehicle_registrations",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "services",
                schema: "tehnicni",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    assigned_to = table.Column<Guid>(type: "uuid", nullable: true),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    vehicle_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    notes = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<string>(type: "text", nullable: false, defaultValue: "open")
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
                name: "vehicle_homologations",
                schema: "tehnicni",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    handled_by = table.Column<Guid>(type: "uuid", nullable: true),
                    service_id = table.Column<Guid>(type: "uuid", nullable: true),
                    vehicle_id = table.Column<Guid>(type: "uuid", nullable: false),
                    document_no = table.Column<string>(type: "text", nullable: true),
                    issued_at = table.Column<DateOnly>(type: "date", nullable: true),
                    kind = table.Column<string>(type: "text", nullable: false),
                    notes = table.Column<string>(type: "text", nullable: true),
                    valid_until = table.Column<DateOnly>(type: "date", nullable: true)
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
                    performed_by = table.Column<Guid>(type: "uuid", nullable: true),
                    service_id = table.Column<Guid>(type: "uuid", nullable: true),
                    vehicle_id = table.Column<Guid>(type: "uuid", nullable: false),
                    notes = table.Column<string>(type: "text", nullable: true),
                    odometer_km = table.Column<int>(type: "integer", nullable: true),
                    performed_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    result = table.Column<string>(type: "text", nullable: false),
                    valid_until = table.Column<DateOnly>(type: "date", nullable: true)
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
                    service_id = table.Column<Guid>(type: "uuid", nullable: true),
                    vehicle_id = table.Column<Guid>(type: "uuid", nullable: false),
                    coverage_type = table.Column<string>(type: "text", nullable: true),
                    insurer_name = table.Column<string>(type: "text", nullable: false),
                    notes = table.Column<string>(type: "text", nullable: true),
                    policy_number = table.Column<string>(type: "text", nullable: true),
                    valid_from = table.Column<DateOnly>(type: "date", nullable: false),
                    valid_to = table.Column<DateOnly>(type: "date", nullable: false)
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
                name: "service_tasks",
                schema: "tehnicni",
                columns: table => new
                {
                    service_id = table.Column<Guid>(type: "uuid", nullable: false),
                    task_id = table.Column<Guid>(type: "uuid", nullable: false),
                    performed_by = table.Column<Guid>(type: "uuid", nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true),
                    performed_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    status = table.Column<string>(type: "text", nullable: false, defaultValue: "pending")
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

            migrationBuilder.CreateIndex(
                name: "IX_vehicles_plate_number",
                schema: "tehnicni",
                table: "vehicles",
                column: "plate_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_registrations_service_id",
                schema: "tehnicni",
                table: "vehicle_registrations",
                column: "service_id");

            migrationBuilder.AddCheckConstraint(
                name: "vehicle_registrations_valid_range_check",
                schema: "tehnicni",
                table: "vehicle_registrations",
                sql: "\"valid_to\" >= \"valid_from\"");

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

            migrationBuilder.AddForeignKey(
                name: "FK_vehicle_registrations_services_service_id",
                schema: "tehnicni",
                table: "vehicle_registrations",
                column: "service_id",
                principalSchema: "tehnicni",
                principalTable: "services",
                principalColumn: "id");
        }
    }
}
