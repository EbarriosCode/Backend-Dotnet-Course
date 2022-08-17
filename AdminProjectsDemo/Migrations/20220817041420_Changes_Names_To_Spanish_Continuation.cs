using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminProjectsDemo.Migrations
{
    public partial class Changes_Names_To_Spanish_Continuation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actividades_Proyectos_ProyectoID",
                table: "Actividades");

            migrationBuilder.DropTable(
                name: "ProjectsExecutors");

            migrationBuilder.DropTable(
                name: "Executors");

            migrationBuilder.RenameColumn(
                name: "ProyectoID",
                table: "Actividades",
                newName: "Proyecto_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Actividades_ProyectoID",
                table: "Actividades",
                newName: "IX_Actividades_Proyecto_Id");

            migrationBuilder.CreateTable(
                name: "Ejecutores",
                columns: table => new
                {
                    Ejecutor_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ejecutores", x => x.Ejecutor_Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProyectosEjecutores",
                columns: table => new
                {
                    Proyecto_Id = table.Column<int>(type: "int", nullable: false),
                    Ejecutor_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProyectosEjecutores", x => new { x.Proyecto_Id, x.Ejecutor_Id });
                    table.ForeignKey(
                        name: "FK_ProyectosEjecutores_Ejecutores_Ejecutor_Id",
                        column: x => x.Ejecutor_Id,
                        principalTable: "Ejecutores",
                        principalColumn: "Ejecutor_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProyectosEjecutores_Proyectos_Proyecto_Id",
                        column: x => x.Proyecto_Id,
                        principalTable: "Proyectos",
                        principalColumn: "Proyecto_Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ProyectosEjecutores_Ejecutor_Id",
                table: "ProyectosEjecutores",
                column: "Ejecutor_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Actividades_Proyectos_Proyecto_Id",
                table: "Actividades",
                column: "Proyecto_Id",
                principalTable: "Proyectos",
                principalColumn: "Proyecto_Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actividades_Proyectos_Proyecto_Id",
                table: "Actividades");

            migrationBuilder.DropTable(
                name: "ProyectosEjecutores");

            migrationBuilder.DropTable(
                name: "Ejecutores");

            migrationBuilder.RenameColumn(
                name: "Proyecto_Id",
                table: "Actividades",
                newName: "ProyectoID");

            migrationBuilder.RenameIndex(
                name: "IX_Actividades_Proyecto_Id",
                table: "Actividades",
                newName: "IX_Actividades_ProyectoID");

            migrationBuilder.CreateTable(
                name: "Executors",
                columns: table => new
                {
                    ExecutorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Executors", x => x.ExecutorId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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
                        name: "FK_ProjectsExecutors_Proyectos_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Proyectos",
                        principalColumn: "Proyecto_Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectsExecutors_ExecutorId",
                table: "ProjectsExecutors",
                column: "ExecutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actividades_Proyectos_ProyectoID",
                table: "Actividades",
                column: "ProyectoID",
                principalTable: "Proyectos",
                principalColumn: "Proyecto_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
