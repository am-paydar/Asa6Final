using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonEntity",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceivedID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRemove = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Flag = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    NormalPath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    BigPath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TinyPath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonEntity", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProductEntity",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceivedID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRemove = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Flag = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    NormalPath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    BigPath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TinyPath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductEntity", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonEntity_ReceivedID",
                table: "PersonEntity",
                column: "ReceivedID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductEntity_ReceivedID",
                table: "ProductEntity",
                column: "ReceivedID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonEntity");

            migrationBuilder.DropTable(
                name: "ProductEntity");
        }
    }
}
