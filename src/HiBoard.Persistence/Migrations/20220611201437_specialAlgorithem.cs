using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiBoard.Persistence.Migrations
{
    public partial class specialAlgorithem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "time_took_to_complete_in_ticks",
                table: "user_activities",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "user_average_time_in_ticks",
                table: "activities",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "user_completed_count",
                table: "activities",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "time_took_to_complete_in_ticks",
                table: "user_activities");

            migrationBuilder.DropColumn(
                name: "user_average_time_in_ticks",
                table: "activities");

            migrationBuilder.DropColumn(
                name: "user_completed_count",
                table: "activities");
        }
    }
}
