using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyBeautyASP.Migrations
{
    public partial class MakeDownloadLinkNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductDownloadLink",
                table: "Products",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductDownloadLink",
                table: "Products");
        }
    }
}
