using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace REAL_ESTATE_CLEAN.Migrations
{
    /// <inheritdoc />
    public partial class AddOperationsReliabilityAlerts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PropertyMatchSalesAutomationOperationsReliabilityAlerts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AnomalyId = table.Column<Guid>(type: "TEXT", nullable: true),
                    AlertNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Severity = table.Column<string>(type: "TEXT", nullable: false),
                    Priority = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Reason = table.Column<string>(type: "TEXT", nullable: false),
                    Score = table.Column<decimal>(type: "TEXT", nullable: false),
                    ScoreDrop = table.Column<decimal>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    AcknowledgedBy = table.Column<string>(type: "TEXT", nullable: true),
                    AcknowledgedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ResolvedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ResolvedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyMatchSalesAutomationOperationsReliabilityAlerts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropertyMatchSalesAutomationOperationsReliabilityAnomalyHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Severity = table.Column<string>(type: "TEXT", nullable: false),
                    PreviousScore = table.Column<decimal>(type: "TEXT", nullable: false),
                    CurrentScore = table.Column<decimal>(type: "TEXT", nullable: false),
                    ScoreChange = table.Column<decimal>(type: "TEXT", nullable: false),
                    ScoreDrop = table.Column<decimal>(type: "TEXT", nullable: false),
                    Reason = table.Column<string>(type: "TEXT", nullable: false),
                    ReasonsJson = table.Column<string>(type: "TEXT", nullable: false),
                    PreviousSnapshotAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CurrentSnapshotAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    DetectedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ResolvedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyMatchSalesAutomationOperationsReliabilityAnomalyHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropertyMatchSalesAutomationOperationsReliabilityHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Score = table.Column<decimal>(type: "TEXT", nullable: false),
                    Grade = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    TotalPenalty = table.Column<decimal>(type: "TEXT", nullable: false),
                    Bonus = table.Column<decimal>(type: "TEXT", nullable: false),
                    RiskScore = table.Column<decimal>(type: "TEXT", nullable: false),
                    OpenIncidents = table.Column<int>(type: "INTEGER", nullable: false),
                    CriticalIncidents = table.Column<int>(type: "INTEGER", nullable: false),
                    SlaBreaches = table.Column<int>(type: "INTEGER", nullable: false),
                    Level2Escalations = table.Column<int>(type: "INTEGER", nullable: false),
                    Level3Escalations = table.Column<int>(type: "INTEGER", nullable: false),
                    MttrMinutes = table.Column<decimal>(type: "TEXT", nullable: false),
                    ResolutionRate = table.Column<decimal>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyMatchSalesAutomationOperationsReliabilityHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropertyMatchSalesAutomationRetryIncidentEscalationHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IncidentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    IncidentNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Action = table.Column<string>(type: "TEXT", nullable: false),
                    PreviousLevel = table.Column<string>(type: "TEXT", nullable: true),
                    NewLevel = table.Column<string>(type: "TEXT", nullable: false),
                    PreviousLevelNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    NewLevelNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    Severity = table.Column<string>(type: "TEXT", nullable: false),
                    OverdueMinutes = table.Column<int>(type: "INTEGER", nullable: false),
                    PerformedBy = table.Column<string>(type: "TEXT", nullable: true),
                    Message = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyMatchSalesAutomationRetryIncidentEscalationHistories", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyMatchSalesAutomationOperationsReliabilityAlerts");

            migrationBuilder.DropTable(
                name: "PropertyMatchSalesAutomationOperationsReliabilityAnomalyHistories");

            migrationBuilder.DropTable(
                name: "PropertyMatchSalesAutomationOperationsReliabilityHistories");

            migrationBuilder.DropTable(
                name: "PropertyMatchSalesAutomationRetryIncidentEscalationHistories");
        }
    }
}
