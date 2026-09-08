using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace REAL_ESTATE_CLEAN.Migrations
{
    /// <inheritdoc />
    public partial class AddOperationsReliabilityAlertHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PropertyMatchSalesAutomationOperationsHealthIncidents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IncidentNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Severity = table.Column<string>(type: "TEXT", nullable: false),
                    Reason = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    FirstScore = table.Column<decimal>(type: "TEXT", nullable: false),
                    LatestScore = table.Column<decimal>(type: "TEXT", nullable: false),
                    PeakScore = table.Column<decimal>(type: "TEXT", nullable: false),
                    ScoreDrop = table.Column<decimal>(type: "TEXT", nullable: false),
                    SnapshotCount = table.Column<int>(type: "INTEGER", nullable: false),
                    WindowStart = table.Column<DateTime>(type: "TEXT", nullable: false),
                    WindowEnd = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ResolvedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyMatchSalesAutomationOperationsHealthIncidents", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyMatchSalesAutomationOperationsHealthIncidents");
        }
    }
}
