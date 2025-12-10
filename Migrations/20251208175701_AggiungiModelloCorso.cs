using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScuolaMVC.Migrations
{
    /// <inheritdoc />
    public partial class AggiungiModelloCorso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Corsi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Corsi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentiCorsi",
                columns: table => new
                {
                    StudenteId = table.Column<int>(type: "int", nullable: false),
                    CorsoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentiCorsi", x => new { x.StudenteId, x.CorsoId });
                    table.ForeignKey(
                        name: "FK_StudentiCorsi_Corsi_CorsoId",
                        column: x => x.CorsoId,
                        principalTable: "Corsi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentiCorsi_Studenti_StudenteId",
                        column: x => x.StudenteId,
                        principalTable: "Studenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentiCorsi_CorsoId",
                table: "StudentiCorsi",
                column: "CorsoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentiCorsi");

            migrationBuilder.DropTable(
                name: "Corsi");
        }
    }
}
