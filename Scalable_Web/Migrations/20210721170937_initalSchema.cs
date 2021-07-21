using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Scalable_Web.Migrations
{
    public partial class initalSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Differences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Left = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Right = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Differences", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Differences");
        }
    }
}
