using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class RenameCustomersToPartiesAndRefreshSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                TRUNCATE TABLE
                    tehnicni.audit_log,
                    tehnicni.homologations,
                    tehnicni.inspections,
                    tehnicni.plate_assignments,
                    tehnicni.insurance_policies,
                    tehnicni.vehicle_registrations,
                    tehnicni.vehicle_ownerships,
                    tehnicni.visit_operations,
                    tehnicni.visits,
                    tehnicni.plates,
                    tehnicni.vehicles,
                    tehnicni.people,
                    tehnicni.companies,
                    tehnicni.customers
                RESTART IDENTITY CASCADE;
                """);

            migrationBuilder.DropForeignKey(
                name: "FK_companies_customers_customer_id",
                schema: "tehnicni",
                table: "companies");

            migrationBuilder.DropForeignKey(
                name: "FK_insurance_policies_customers_customer_id",
                schema: "tehnicni",
                table: "insurance_policies");

            migrationBuilder.DropForeignKey(
                name: "FK_people_customers_customer_id",
                schema: "tehnicni",
                table: "people");

            migrationBuilder.DropForeignKey(
                name: "FK_vehicle_ownerships_customers_customer_id",
                schema: "tehnicni",
                table: "vehicle_ownerships");

            migrationBuilder.DropForeignKey(
                name: "FK_vehicle_registrations_customers_customer_id",
                schema: "tehnicni",
                table: "vehicle_registrations");

            migrationBuilder.DropForeignKey(
                name: "FK_visits_customers_customer_id",
                schema: "tehnicni",
                table: "visits");

            migrationBuilder.DropTable(
                name: "customers",
                schema: "tehnicni");

            migrationBuilder.RenameColumn(
                name: "customer_id",
                schema: "tehnicni",
                table: "visits",
                newName: "party_id");

            migrationBuilder.RenameIndex(
                name: "IX_visits_customer_id",
                schema: "tehnicni",
                table: "visits",
                newName: "IX_visits_party_id");

            migrationBuilder.RenameColumn(
                name: "customer_id",
                schema: "tehnicni",
                table: "vehicle_registrations",
                newName: "party_id");

            migrationBuilder.RenameIndex(
                name: "IX_vehicle_registrations_customer_id",
                schema: "tehnicni",
                table: "vehicle_registrations",
                newName: "IX_vehicle_registrations_party_id");

            migrationBuilder.RenameColumn(
                name: "customer_id",
                schema: "tehnicni",
                table: "vehicle_ownerships",
                newName: "party_id");

            migrationBuilder.RenameIndex(
                name: "IX_vehicle_ownerships_customer_id",
                schema: "tehnicni",
                table: "vehicle_ownerships",
                newName: "IX_vehicle_ownerships_party_id");

            migrationBuilder.RenameColumn(
                name: "tax_number",
                schema: "tehnicni",
                table: "people",
                newName: "tax_no");

            migrationBuilder.RenameColumn(
                name: "customer_id",
                schema: "tehnicni",
                table: "people",
                newName: "party_id");

            migrationBuilder.RenameColumn(
                name: "customer_id",
                schema: "tehnicni",
                table: "insurance_policies",
                newName: "party_id");

            migrationBuilder.RenameIndex(
                name: "IX_insurance_policies_customer_id",
                schema: "tehnicni",
                table: "insurance_policies",
                newName: "IX_insurance_policies_party_id");

            migrationBuilder.RenameColumn(
                name: "tax_number",
                schema: "tehnicni",
                table: "companies",
                newName: "tax_no");

            migrationBuilder.RenameColumn(
                name: "registration_no",
                schema: "tehnicni",
                table: "companies",
                newName: "company_reg_no");

            migrationBuilder.RenameColumn(
                name: "customer_id",
                schema: "tehnicni",
                table: "companies",
                newName: "party_id");

            migrationBuilder.CreateTable(
                name: "parties",
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
                    table.PrimaryKey("PK_parties", x => x.id);
                    table.CheckConstraint("parties_type_check", "\"type\" IN ('person','company')");
                });

            migrationBuilder.AddForeignKey(
                name: "FK_companies_parties_party_id",
                schema: "tehnicni",
                table: "companies",
                column: "party_id",
                principalSchema: "tehnicni",
                principalTable: "parties",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_insurance_policies_parties_party_id",
                schema: "tehnicni",
                table: "insurance_policies",
                column: "party_id",
                principalSchema: "tehnicni",
                principalTable: "parties",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_people_parties_party_id",
                schema: "tehnicni",
                table: "people",
                column: "party_id",
                principalSchema: "tehnicni",
                principalTable: "parties",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_vehicle_ownerships_parties_party_id",
                schema: "tehnicni",
                table: "vehicle_ownerships",
                column: "party_id",
                principalSchema: "tehnicni",
                principalTable: "parties",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_vehicle_registrations_parties_party_id",
                schema: "tehnicni",
                table: "vehicle_registrations",
                column: "party_id",
                principalSchema: "tehnicni",
                principalTable: "parties",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_visits_parties_party_id",
                schema: "tehnicni",
                table: "visits",
                column: "party_id",
                principalSchema: "tehnicni",
                principalTable: "parties",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_companies_parties_party_id",
                schema: "tehnicni",
                table: "companies");

            migrationBuilder.DropForeignKey(
                name: "FK_insurance_policies_parties_party_id",
                schema: "tehnicni",
                table: "insurance_policies");

            migrationBuilder.DropForeignKey(
                name: "FK_people_parties_party_id",
                schema: "tehnicni",
                table: "people");

            migrationBuilder.DropForeignKey(
                name: "FK_vehicle_ownerships_parties_party_id",
                schema: "tehnicni",
                table: "vehicle_ownerships");

            migrationBuilder.DropForeignKey(
                name: "FK_vehicle_registrations_parties_party_id",
                schema: "tehnicni",
                table: "vehicle_registrations");

            migrationBuilder.DropForeignKey(
                name: "FK_visits_parties_party_id",
                schema: "tehnicni",
                table: "visits");

            migrationBuilder.DropTable(
                name: "parties",
                schema: "tehnicni");

            migrationBuilder.RenameColumn(
                name: "party_id",
                schema: "tehnicni",
                table: "visits",
                newName: "customer_id");

            migrationBuilder.RenameIndex(
                name: "IX_visits_party_id",
                schema: "tehnicni",
                table: "visits",
                newName: "IX_visits_customer_id");

            migrationBuilder.RenameColumn(
                name: "party_id",
                schema: "tehnicni",
                table: "vehicle_registrations",
                newName: "customer_id");

            migrationBuilder.RenameIndex(
                name: "IX_vehicle_registrations_party_id",
                schema: "tehnicni",
                table: "vehicle_registrations",
                newName: "IX_vehicle_registrations_customer_id");

            migrationBuilder.RenameColumn(
                name: "party_id",
                schema: "tehnicni",
                table: "vehicle_ownerships",
                newName: "customer_id");

            migrationBuilder.RenameIndex(
                name: "IX_vehicle_ownerships_party_id",
                schema: "tehnicni",
                table: "vehicle_ownerships",
                newName: "IX_vehicle_ownerships_customer_id");

            migrationBuilder.RenameColumn(
                name: "tax_no",
                schema: "tehnicni",
                table: "people",
                newName: "tax_number");

            migrationBuilder.RenameColumn(
                name: "party_id",
                schema: "tehnicni",
                table: "people",
                newName: "customer_id");

            migrationBuilder.RenameColumn(
                name: "party_id",
                schema: "tehnicni",
                table: "insurance_policies",
                newName: "customer_id");

            migrationBuilder.RenameIndex(
                name: "IX_insurance_policies_party_id",
                schema: "tehnicni",
                table: "insurance_policies",
                newName: "IX_insurance_policies_customer_id");

            migrationBuilder.RenameColumn(
                name: "tax_no",
                schema: "tehnicni",
                table: "companies",
                newName: "tax_number");

            migrationBuilder.RenameColumn(
                name: "company_reg_no",
                schema: "tehnicni",
                table: "companies",
                newName: "registration_no");

            migrationBuilder.RenameColumn(
                name: "party_id",
                schema: "tehnicni",
                table: "companies",
                newName: "customer_id");

            migrationBuilder.CreateTable(
                name: "customers",
                schema: "tehnicni",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    address = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    email = table.Column<string>(type: "text", nullable: true),
                    phone = table.Column<string>(type: "text", nullable: true),
                    type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.id);
                    table.CheckConstraint("customers_type_check", "\"type\" IN ('person','company')");
                });

            migrationBuilder.AddForeignKey(
                name: "FK_companies_customers_customer_id",
                schema: "tehnicni",
                table: "companies",
                column: "customer_id",
                principalSchema: "tehnicni",
                principalTable: "customers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_insurance_policies_customers_customer_id",
                schema: "tehnicni",
                table: "insurance_policies",
                column: "customer_id",
                principalSchema: "tehnicni",
                principalTable: "customers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_people_customers_customer_id",
                schema: "tehnicni",
                table: "people",
                column: "customer_id",
                principalSchema: "tehnicni",
                principalTable: "customers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_vehicle_ownerships_customers_customer_id",
                schema: "tehnicni",
                table: "vehicle_ownerships",
                column: "customer_id",
                principalSchema: "tehnicni",
                principalTable: "customers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_visits_customers_customer_id",
                schema: "tehnicni",
                table: "visits",
                column: "customer_id",
                principalSchema: "tehnicni",
                principalTable: "customers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
