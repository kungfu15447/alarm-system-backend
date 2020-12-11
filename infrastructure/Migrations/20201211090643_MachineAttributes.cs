using Microsoft.EntityFrameworkCore.Migrations;

namespace infrastructure.Migrations
{
    public partial class MachineAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Machines",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Machines",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Machines");
        }
    }
}
