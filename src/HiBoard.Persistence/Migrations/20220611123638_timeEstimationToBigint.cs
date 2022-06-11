using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiBoard.Persistence.Migrations
{
    public partial class timeEstimationToBigint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "time_estimation_in_seconds",
                table: "activities",
                newName: "time_estimation_in_ticks");

            migrationBuilder.AlterColumn<long>(
                name: "time_estimation_in_ticks",
                table: "activities",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "time_estimation_in_ticks",
                table: "activities",
                newName: "time_estimation_in_seconds");

            migrationBuilder.AlterColumn<int>(
                name: "time_estimation_in_seconds",
                table: "activities",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
