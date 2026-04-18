using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentityIndexesAndRefreshSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                TRUNCATE TABLE
                    tehnicni.audit_log,
                    tehnicni.homologations,
                    tehnicni.inspections,
                    tehnicni.insurance_policies,
                    tehnicni.plate_assignments,
                    tehnicni.plates,
                    tehnicni.vehicle_registrations,
                    tehnicni.vehicle_ownerships,
                    tehnicni.visit_operations,
                    tehnicni.visits,
                    tehnicni.vehicles,
                    tehnicni.companies,
                    tehnicni.people,
                    tehnicni.parties,
                    tehnicni.refresh_tokens,
                    tehnicni.user_session_events,
                    tehnicni.user_sessions
                RESTART IDENTITY CASCADE;
                """);

            migrationBuilder.CreateIndex(
                name: "IX_people_emso",
                schema: "tehnicni",
                table: "people",
                column: "emso",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_people_tax_no",
                schema: "tehnicni",
                table: "people",
                column: "tax_no",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_companies_company_reg_no",
                schema: "tehnicni",
                table: "companies",
                column: "company_reg_no",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_companies_tax_no",
                schema: "tehnicni",
                table: "companies",
                column: "tax_no",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_people_emso",
                schema: "tehnicni",
                table: "people");

            migrationBuilder.DropIndex(
                name: "IX_people_tax_no",
                schema: "tehnicni",
                table: "people");

            migrationBuilder.DropIndex(
                name: "IX_companies_company_reg_no",
                schema: "tehnicni",
                table: "companies");

            migrationBuilder.DropIndex(
                name: "IX_companies_tax_no",
                schema: "tehnicni",
                table: "companies");
        }
    }
}
