using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooApi.Migrations
{
    /// <inheritdoc />
    public partial class CreateAnimalCuidadoRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cuidados_Animais_AnimalId",
                table: "Cuidados");

            migrationBuilder.DropIndex(
                name: "IX_Cuidados_AnimalId",
                table: "Cuidados");

            migrationBuilder.DropColumn(
                name: "AnimalId",
                table: "Cuidados");

            migrationBuilder.CreateTable(
                name: "AnimaisCuidados",
                columns: table => new
                {
                    AnimalId = table.Column<int>(type: "int", nullable: false),
                    CuidadoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimaisCuidados", x => new { x.AnimalId, x.CuidadoId });
                    table.ForeignKey(
                        name: "FK_AnimaisCuidados_Animais_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimaisCuidados_Cuidados_CuidadoId",
                        column: x => x.CuidadoId,
                        principalTable: "Cuidados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimaisCuidados_CuidadoId",
                table: "AnimaisCuidados",
                column: "CuidadoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimaisCuidados");

            migrationBuilder.AddColumn<int>(
                name: "AnimalId",
                table: "Cuidados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cuidados_AnimalId",
                table: "Cuidados",
                column: "AnimalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cuidados_Animais_AnimalId",
                table: "Cuidados",
                column: "AnimalId",
                principalTable: "Animais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
