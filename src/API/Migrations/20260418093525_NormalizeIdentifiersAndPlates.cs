using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class NormalizeIdentifiersAndPlates : Migration
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

            migrationBuilder.DropColumn(
                name: "active",
                schema: "tehnicni",
                table: "plates");

            migrationBuilder.DropColumn(
                name: "notes",
                schema: "tehnicni",
                table: "plates");

            migrationBuilder.DropColumn(
                name: "national_no",
                schema: "tehnicni",
                table: "people");

            migrationBuilder.RenameColumn(
                name: "region_code",
                schema: "tehnicni",
                table: "plates",
                newName: "plate_type");

            migrationBuilder.RenameColumn(
                name: "plate_number",
                schema: "tehnicni",
                table: "plates",
                newName: "plate_no");

            migrationBuilder.RenameIndex(
                name: "IX_plates_plate_number",
                schema: "tehnicni",
                table: "plates",
                newName: "IX_plates_plate_no");

            migrationBuilder.AlterColumn<string>(
                name: "tax_no",
                schema: "tehnicni",
                table: "people",
                type: "character varying(8)",
                maxLength: 8,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "emso",
                schema: "tehnicni",
                table: "people",
                type: "character varying(13)",
                maxLength: 13,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tax_no",
                schema: "tehnicni",
                table: "companies",
                type: "character varying(8)",
                maxLength: 8,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "company_reg_no",
                schema: "tehnicni",
                table: "companies",
                type: "character varying(8)",
                maxLength: 8,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddCheckConstraint(
                name: "plates_type_check",
                schema: "tehnicni",
                table: "plates",
                sql: "\"plate_type\" IN ('standard','custom','diplomatic','military','police','temporary','test','export','agricultural','trailer','moped')");

            migrationBuilder.AddCheckConstraint(
                name: "people_emso_format_check",
                schema: "tehnicni",
                table: "people",
                sql: "\"emso\" IS NULL OR \"emso\" ~ '^[0-9]{13}$'");

            migrationBuilder.AddCheckConstraint(
                name: "people_tax_no_format_check",
                schema: "tehnicni",
                table: "people",
                sql: "\"tax_no\" IS NULL OR \"tax_no\" ~ '^[0-9]{8}$'");

            migrationBuilder.AddCheckConstraint(
                name: "companies_company_reg_no_format_check",
                schema: "tehnicni",
                table: "companies",
                sql: "\"company_reg_no\" IS NULL OR \"company_reg_no\" ~ '^[0-9]{8}$'");

            migrationBuilder.AddCheckConstraint(
                name: "companies_tax_no_format_check",
                schema: "tehnicni",
                table: "companies",
                sql: "\"tax_no\" IS NULL OR \"tax_no\" ~ '^[0-9]{8}$'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "plates_type_check",
                schema: "tehnicni",
                table: "plates");

            migrationBuilder.DropCheckConstraint(
                name: "people_emso_format_check",
                schema: "tehnicni",
                table: "people");

            migrationBuilder.DropCheckConstraint(
                name: "people_tax_no_format_check",
                schema: "tehnicni",
                table: "people");

            migrationBuilder.DropCheckConstraint(
                name: "companies_company_reg_no_format_check",
                schema: "tehnicni",
                table: "companies");

            migrationBuilder.DropCheckConstraint(
                name: "companies_tax_no_format_check",
                schema: "tehnicni",
                table: "companies");

            migrationBuilder.DropColumn(
                name: "emso",
                schema: "tehnicni",
                table: "people");

            migrationBuilder.RenameColumn(
                name: "plate_type",
                schema: "tehnicni",
                table: "plates",
                newName: "region_code");

            migrationBuilder.RenameColumn(
                name: "plate_no",
                schema: "tehnicni",
                table: "plates",
                newName: "plate_number");

            migrationBuilder.RenameIndex(
                name: "IX_plates_plate_no",
                schema: "tehnicni",
                table: "plates",
                newName: "IX_plates_plate_number");

            migrationBuilder.AddColumn<bool>(
                name: "active",
                schema: "tehnicni",
                table: "plates",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "notes",
                schema: "tehnicni",
                table: "plates",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tax_no",
                schema: "tehnicni",
                table: "people",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(8)",
                oldMaxLength: 8,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "national_no",
                schema: "tehnicni",
                table: "people",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tax_no",
                schema: "tehnicni",
                table: "companies",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(8)",
                oldMaxLength: 8,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "company_reg_no",
                schema: "tehnicni",
                table: "companies",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(8)",
                oldMaxLength: 8,
                oldNullable: true);
        }
    }
}
