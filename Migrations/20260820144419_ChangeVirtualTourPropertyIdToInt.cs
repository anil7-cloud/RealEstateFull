using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace REAL_ESTATE_CLEAN.Migrations
{
    /// <inheritdoc />
    public partial class ChangeVirtualTourPropertyIdToInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PropertyId",
                table: "PropertyVirtualTours",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "PropertyId",
                table: "PropertyVirtualTours",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }
    }
}
