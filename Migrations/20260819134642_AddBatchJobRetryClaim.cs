using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace REAL_ESTATE_CLEAN.Migrations
{
    /// <inheritdoc />
    public partial class AddBatchJobRetryClaim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RetryClaimToken",
                table: "PropertyMatchSalesAutomationBatchJobs",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RetryClaimed",
                table: "PropertyMatchSalesAutomationBatchJobs",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "RetryClaimedAt",
                table: "PropertyMatchSalesAutomationBatchJobs",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RetryClaimToken",
                table: "PropertyMatchSalesAutomationBatchJobs");

            migrationBuilder.DropColumn(
                name: "RetryClaimed",
                table: "PropertyMatchSalesAutomationBatchJobs");

            migrationBuilder.DropColumn(
                name: "RetryClaimedAt",
                table: "PropertyMatchSalesAutomationBatchJobs");
        }
    }
}
