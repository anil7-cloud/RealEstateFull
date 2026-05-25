using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateApp.Web.Migrations
{
    /// <inheritdoc />
    public partial class AgencyIdentityUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agencies_User_UserId",
                table: "Agencies");

            migrationBuilder.DropForeignKey(
                name: "FK_Leads_Listings_ListingId",
                table: "Leads");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_Agencies_UserId",
                table: "Agencies");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Agencies");

            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "Agencies",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Leads_Listings_ListingId",
                table: "Leads",
                column: "ListingId",
                principalTable: "Listings",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leads_Listings_ListingId",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "Agencies");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Agencies",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    FullName = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agencies_UserId",
                table: "Agencies",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Agencies_User_UserId",
                table: "Agencies",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Leads_Listings_ListingId",
                table: "Leads",
                column: "ListingId",
                principalTable: "Listings",
                principalColumn: "Id");
        }
    }
}
