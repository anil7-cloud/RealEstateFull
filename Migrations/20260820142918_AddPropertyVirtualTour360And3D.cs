using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace REAL_ESTATE_CLEAN.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertyVirtualTour360And3D : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PropertyVirtualTours",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PropertyId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    TourType = table.Column<string>(type: "TEXT", nullable: false),
                    ThumbnailUrl = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsPublished = table.Column<bool>(type: "INTEGER", nullable: false),
                    DisplayOrder = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyVirtualTours", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropertyThreeDimensionalModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    VirtualTourId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ModelUrl = table.Column<string>(type: "TEXT", nullable: false),
                    Format = table.Column<string>(type: "TEXT", nullable: false),
                    PosterUrl = table.Column<string>(type: "TEXT", nullable: true),
                    AllowRotation = table.Column<bool>(type: "INTEGER", nullable: false),
                    AllowZoom = table.Column<bool>(type: "INTEGER", nullable: false),
                    AutoRotate = table.Column<bool>(type: "INTEGER", nullable: false),
                    AutoRotateSpeed = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyThreeDimensionalModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyThreeDimensionalModels_PropertyVirtualTours_VirtualTourId",
                        column: x => x.VirtualTourId,
                        principalTable: "PropertyVirtualTours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyVirtualTourScenes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    VirtualTourId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    PanoramaUrl = table.Column<string>(type: "TEXT", nullable: false),
                    ThumbnailUrl = table.Column<string>(type: "TEXT", nullable: true),
                    InitialYaw = table.Column<decimal>(type: "TEXT", nullable: false),
                    InitialPitch = table.Column<decimal>(type: "TEXT", nullable: false),
                    InitialFov = table.Column<decimal>(type: "TEXT", nullable: false),
                    DisplayOrder = table.Column<int>(type: "INTEGER", nullable: false),
                    IsStartScene = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyVirtualTourScenes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyVirtualTourScenes_PropertyVirtualTours_VirtualTourId",
                        column: x => x.VirtualTourId,
                        principalTable: "PropertyVirtualTours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyVirtualTourHotspots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SceneId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    HotspotType = table.Column<string>(type: "TEXT", nullable: false),
                    TargetSceneId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Yaw = table.Column<decimal>(type: "TEXT", nullable: false),
                    Pitch = table.Column<decimal>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyVirtualTourHotspots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyVirtualTourHotspots_PropertyVirtualTourScenes_SceneId",
                        column: x => x.SceneId,
                        principalTable: "PropertyVirtualTourScenes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyThreeDimensionalModels_VirtualTourId",
                table: "PropertyThreeDimensionalModels",
                column: "VirtualTourId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyVirtualTourHotspots_SceneId",
                table: "PropertyVirtualTourHotspots",
                column: "SceneId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyVirtualTourScenes_VirtualTourId",
                table: "PropertyVirtualTourScenes",
                column: "VirtualTourId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyThreeDimensionalModels");

            migrationBuilder.DropTable(
                name: "PropertyVirtualTourHotspots");

            migrationBuilder.DropTable(
                name: "PropertyVirtualTourScenes");

            migrationBuilder.DropTable(
                name: "PropertyVirtualTours");
        }
    }
}
