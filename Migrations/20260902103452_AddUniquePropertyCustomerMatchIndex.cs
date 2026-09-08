using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace REAL_ESTATE_CLEAN.Migrations
{
    /// <inheritdoc />
    public partial class AddUniquePropertyCustomerMatchIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PropertyCustomerMatches_PropertyId_LeadId",
                table: "PropertyCustomerMatches",
                columns: new[] { "PropertyId", "LeadId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PropertyCustomerMatches_PropertyId_LeadId",
                table: "PropertyCustomerMatches");
        }
    }
}
