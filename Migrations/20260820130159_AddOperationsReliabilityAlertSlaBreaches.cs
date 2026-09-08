using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace REAL_ESTATE_CLEAN.Migrations
{
    /// <inheritdoc />
    public partial class AddOperationsReliabilityAlertSlaBreaches : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PropertyMatchSalesAutomationOperationsReliabilityAlertHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AlertId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AlertNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Action = table.Column<string>(type: "TEXT", nullable: false),
                    PreviousStatus = table.Column<string>(type: "TEXT", nullable: true),
                    NewStatus = table.Column<string>(type: "TEXT", nullable: false),
                    Severity = table.Column<string>(type: "TEXT", nullable: false),
                    Priority = table.Column<string>(type: "TEXT", nullable: false),
                    PerformedBy = table.Column<string>(type: "TEXT", nullable: true),
                    Message = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyMatchSalesAutomationOperationsReliabilityAlertHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropertyMatchSalesAutomationOperationsReliabilityAlertSlaBreaches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AlertId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AlertNumber = table.Column<string>(type: "TEXT", nullable: false),
                    BreachType = table.Column<string>(type: "TEXT", nullable: false),
                    Priority = table.Column<string>(type: "TEXT", nullable: false),
                    Severity = table.Column<string>(type: "TEXT", nullable: false),
                    LimitMinutes = table.Column<decimal>(type: "TEXT", nullable: false),
                    ActualMinutes = table.Column<decimal>(type: "TEXT", nullable: false),
                    OverdueMinutes = table.Column<decimal>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    DetectedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ResolvedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyMatchSalesAutomationOperationsReliabilityAlertSlaBreaches", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyMatchSalesAutomationOperationsReliabilityAlertHistories");

            migrationBuilder.DropTable(
                name: "PropertyMatchSalesAutomationOperationsReliabilityAlertSlaBreaches");
        }
    }
}
