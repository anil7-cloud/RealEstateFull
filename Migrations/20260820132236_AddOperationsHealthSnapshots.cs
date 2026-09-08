using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace REAL_ESTATE_CLEAN.Migrations
{
    /// <inheritdoc />
    public partial class AddOperationsHealthSnapshots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PropertyMatchSalesAutomationOperationsHealthSnapshots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Score = table.Column<decimal>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    ReliabilityScore = table.Column<decimal>(type: "TEXT", nullable: false),
                    AnomalyPenalty = table.Column<decimal>(type: "TEXT", nullable: false),
                    EscalationPenalty = table.Column<decimal>(type: "TEXT", nullable: false),
                    HasAnomaly = table.Column<bool>(type: "INTEGER", nullable: false),
                    AnomalySeverity = table.Column<string>(type: "TEXT", nullable: false),
                    ActiveEscalations = table.Column<int>(type: "INTEGER", nullable: false),
                    HighestEscalationLevel = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyMatchSalesAutomationOperationsHealthSnapshots", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyMatchSalesAutomationOperationsHealthSnapshots");
        }
    }
}
