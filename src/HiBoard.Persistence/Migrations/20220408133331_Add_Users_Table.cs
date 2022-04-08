using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiBoard.Persistence.Migrations
{
    public partial class Add_Users_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    role = table.Column<int>(type: "int", nullable: false),
                    department = table.Column<int>(type: "int", nullable: false),
                    creation_date = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    is_deleted = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
