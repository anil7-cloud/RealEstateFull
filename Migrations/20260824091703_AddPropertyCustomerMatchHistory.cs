using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace REAL_ESTATE_CLEAN.Migrations
{
    public partial class AddPropertyCustomerMatchHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "PropertyMedia",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PropertyMedia",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "PropertyMedia",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPrimary",
                table: "PropertyMedia",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MediaUrl",
                table: "PropertyMedia",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "PropertyMedia",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "PropertyMedia",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "PropertyMedia");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "PropertyMedia");

            migrationBuilder.DropColumn(
                name: "IsPrimary",
                table: "PropertyMedia");

            migrationBuilder.DropColumn(
                name: "MediaUrl",
                table: "PropertyMedia");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "PropertyMedia");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "PropertyMedia");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "PropertyMedia",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);
        }
    }
}
