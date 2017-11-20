using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Vega.Migrations
{
    public partial class PopulateFeatures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO FeatureSet (Name) VALUES ('Feature1')");
            migrationBuilder.Sql("INSERT INTO FeatureSet (Name) VALUES ('Feature2')");

            migrationBuilder.Sql("INSERT INTO FeatureSet (Name) VALUES ('Feature3')");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
