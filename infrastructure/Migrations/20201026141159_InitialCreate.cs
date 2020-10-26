using Microsoft.EntityFrameworkCore.Migrations;

namespace infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alarms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alarms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Machines",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlarmWatch",
                columns: table => new
                {
                    AlarmId = table.Column<int>(nullable: false),
                    WatchId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmWatch", x => new { x.AlarmId, x.WatchId });
                    table.ForeignKey(
                        name: "FK_AlarmWatch_Alarms_AlarmId",
                        column: x => x.AlarmId,
                        principalTable: "Alarms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlarmLogs",
                columns: table => new
                {
                    MachineId = table.Column<string>(nullable: false),
                    AlarmId = table.Column<int>(nullable: false),
                    Date = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmLogs", x => new { x.AlarmId, x.MachineId, x.Date });
                    table.ForeignKey(
                        name: "FK_AlarmLogs_Alarms_AlarmId",
                        column: x => x.AlarmId,
                        principalTable: "Alarms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlarmLogs_Machines_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MachineWatch",
                columns: table => new
                {
                    MachineId = table.Column<string>(nullable: false),
                    WatchId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineWatch", x => new { x.MachineId, x.WatchId });
                    table.ForeignKey(
                        name: "FK_MachineWatch_Machines_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlarmLogs_MachineId",
                table: "AlarmLogs",
                column: "MachineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlarmLogs");

            migrationBuilder.DropTable(
                name: "AlarmWatch");

            migrationBuilder.DropTable(
                name: "MachineWatch");

            migrationBuilder.DropTable(
                name: "Alarms");

            migrationBuilder.DropTable(
                name: "Machines");
        }
    }
}
