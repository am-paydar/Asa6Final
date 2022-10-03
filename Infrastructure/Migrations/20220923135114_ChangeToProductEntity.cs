using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class ChangeToProductEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductEntity_ReceivedID",
                table: "ProductEntity");

            migrationBuilder.CreateIndex(
                name: "IX_ProductEntity_ReceivedID",
                table: "ProductEntity",
                column: "ReceivedID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductEntity_ReceivedID",
                table: "ProductEntity");

            migrationBuilder.CreateIndex(
                name: "IX_ProductEntity_ReceivedID",
                table: "ProductEntity",
                column: "ReceivedID",
                unique: true);
        }
    }
}
