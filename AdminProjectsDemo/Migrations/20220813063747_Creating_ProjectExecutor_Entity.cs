using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminProjectsDemo.Migrations
{
    public partial class Creating_ProjectExecutor_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectsExecutors",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    ExecutorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectsExecutors", x => new { x.ProjectId, x.ExecutorId });
                    table.ForeignKey(
                        name: "FK_ProjectsExecutors_Executors_ExecutorId",
                        column: x => x.ExecutorId,
                        principalTable: "Executors",
                        principalColumn: "ExecutorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectsExecutors_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectsExecutors_ExecutorId",
                table: "ProjectsExecutors",
                column: "ExecutorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectsExecutors");
        }
    }
}
