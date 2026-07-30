using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaEscolarCompleto.Migrations
{
    /// <inheritdoc />
    public partial class HorarioAulaSemanal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HorariosAula",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TurmaId = table.Column<int>(type: "int", nullable: false),
                    MateriaId = table.Column<int>(type: "int", nullable: false),
                    ProfessorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DiaSemana = table.Column<int>(type: "int", nullable: false),
                    NumeroAula = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorariosAula", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HorariosAula_AspNetUsers_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HorariosAula_Materias_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "Materias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HorariosAula_Turmas_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turmas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HorariosAula_MateriaId",
                table: "HorariosAula",
                column: "MateriaId");

            migrationBuilder.CreateIndex(
                name: "IX_HorariosAula_ProfessorId",
                table: "HorariosAula",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_HorariosAula_TurmaId_DiaSemana_NumeroAula",
                table: "HorariosAula",
                columns: new[] { "TurmaId", "DiaSemana", "NumeroAula" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HorariosAula");
        }
    }
}
