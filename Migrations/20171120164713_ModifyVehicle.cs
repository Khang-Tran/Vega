using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Vega.Migrations
{
    public partial class ModifyVehicle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ContactPhone",
                table: "VehicleSet",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContactName",
                table: "VehicleSet",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "VehicleSet",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsRegister",
                table: "VehicleSet",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "VehicleFeatureSet",
                columns: table => new
                {
                    FeatureId = table.Column<int>(nullable: false),
                    VehicleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleFeatureSet", x => new { x.FeatureId, x.VehicleId });
                    table.ForeignKey(
                        name: "FK_VehicleFeatureSet_FeatureSet_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "FeatureSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehicleFeatureSet_VehicleSet_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "VehicleSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleFeatureSet_VehicleId",
                table: "VehicleFeatureSet",
                column: "VehicleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleFeatureSet");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "VehicleSet");

            migrationBuilder.DropColumn(
                name: "IsRegister",
                table: "VehicleSet");

            migrationBuilder.AlterColumn<string>(
                name: "ContactPhone",
                table: "VehicleSet",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "ContactName",
                table: "VehicleSet",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255);
        }
    }
}
