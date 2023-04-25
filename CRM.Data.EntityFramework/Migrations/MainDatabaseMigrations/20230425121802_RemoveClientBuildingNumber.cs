using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.EntityFramework.Migrations.MainDatabaseMigrations
{
    public partial class RemoveClientBuildingNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuildingNumber",
                table: "Clients");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BuildingNumber",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
