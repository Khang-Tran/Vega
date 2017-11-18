using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Vega.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO MakeSet (Name) VALUES ('Make1')");
            migrationBuilder.Sql("INSERT INTO MakeSet (Name) VALUES ('Make2')");
            migrationBuilder.Sql("INSERT INTO MakeSet (Name) VALUES ('Make3')");

            migrationBuilder.Sql("INSERT INTO ModelSet (Name, MakeId) VALUES ('Make1-ModelA', (SELECT ID FROM MakeSet WHERE Name='Make1'))");
            migrationBuilder.Sql("INSERT INTO ModelSet (Name, MakeId) VALUES ('Make1-ModelB', (SELECT ID FROM MakeSet WHERE Name='Make1'))");
            migrationBuilder.Sql("INSERT INTO ModelSet (Name, MakeId) VALUES ('Make1-ModelC', (SELECT ID FROM MakeSet WHERE Name='Make1'))");

            migrationBuilder.Sql("INSERT INTO ModelSet (Name, MakeId) VALUES ('Make1-ModelA', (SELECT ID FROM MakeSet WHERE Name='Make2'))");
            migrationBuilder.Sql("INSERT INTO ModelSet (Name, MakeId) VALUES ('Make1-ModelB', (SELECT ID FROM MakeSet WHERE Name='Make2'))");
            migrationBuilder.Sql("INSERT INTO ModelSet (Name, MakeId) VALUES ('Make1-ModelC', (SELECT ID FROM MakeSet WHERE Name='Make2'))");

            migrationBuilder.Sql("INSERT INTO ModelSet (Name, MakeId) VALUES ('Make1-ModelA', (SELECT ID FROM MakeSet WHERE Name='Make3'))");
            migrationBuilder.Sql("INSERT INTO ModelSet (Name, MakeId) VALUES ('Make1-ModelB', (SELECT ID FROM MakeSet WHERE Name='Make3'))");
            migrationBuilder.Sql("INSERT INTO ModelSet (Name, MakeId) VALUES ('Make1-ModelC', (SELECT ID FROM MakeSet WHERE Name='Make3'))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM MakeSet");
        }
    }
}
