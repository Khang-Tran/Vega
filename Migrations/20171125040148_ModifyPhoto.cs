using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Vega.Migrations
{
    public partial class ModifyPhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photo_VehicleSet_VehicleId",
                table: "Photo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Photo",
                table: "Photo");

            migrationBuilder.RenameTable(
                name: "Photo",
                newName: "PhotoSet");

            migrationBuilder.RenameIndex(
                name: "IX_Photo_VehicleId",
                table: "PhotoSet",
                newName: "IX_PhotoSet_VehicleId");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleId",
                table: "PhotoSet",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhotoSet",
                table: "PhotoSet",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoSet_VehicleSet_VehicleId",
                table: "PhotoSet",
                column: "VehicleId",
                principalTable: "VehicleSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotoSet_VehicleSet_VehicleId",
                table: "PhotoSet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhotoSet",
                table: "PhotoSet");

            migrationBuilder.RenameTable(
                name: "PhotoSet",
                newName: "Photo");

            migrationBuilder.RenameIndex(
                name: "IX_PhotoSet_VehicleId",
                table: "Photo",
                newName: "IX_Photo_VehicleId");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleId",
                table: "Photo",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Photo",
                table: "Photo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_VehicleSet_VehicleId",
                table: "Photo",
                column: "VehicleId",
                principalTable: "VehicleSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
