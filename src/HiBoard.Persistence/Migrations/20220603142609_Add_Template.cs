using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiBoard.Persistence.Migrations
{
    public partial class Add_Template : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "templates",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    creation_at = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    updated_at = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_deleted = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_templates", x => x.id);
                    table.ForeignKey(
                        name: "FK_templates_companies_company_id",
                        column: x => x.company_id,
                        principalTable: "companies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "template_activities",
                columns: table => new
                {
                    ActivityId = table.Column<int>(type: "int", nullable: false),
                    TemplateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_template_activities", x => new { x.ActivityId, x.TemplateId });
                    table.ForeignKey(
                        name: "FK_template_activities_activities_ActivitiesId",
                        column: x => x.ActivityId,
                        principalTable: "activities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_template_activities_templates_TemplatesId",
                        column: x => x.TemplateId,
                        principalTable: "templates",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_template_activities_TemplatesId",
                table: "template_activities",
                column: "TemplatesId");

            migrationBuilder.CreateIndex(
                name: "IX_templates_company_id",
                table: "templates",
                column: "company_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "template_activities");

            migrationBuilder.DropTable(
                name: "templates");
        }
    }
}
