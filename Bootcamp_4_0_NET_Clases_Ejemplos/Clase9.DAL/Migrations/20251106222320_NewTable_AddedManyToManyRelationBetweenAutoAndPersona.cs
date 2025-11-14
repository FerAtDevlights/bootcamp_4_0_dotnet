using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bootcamp.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class NewTable_AddedManyToManyRelationBetweenAutoAndPersona : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Autos_Personas_PersonaId",
                table: "Autos");

            migrationBuilder.DropIndex(
                name: "IX_Autos_PersonaId",
                table: "Autos");

            migrationBuilder.DropColumn(
                name: "PersonaId",
                table: "Autos");

            migrationBuilder.CreateTable(
                name: "AutoPersona",
                columns: table => new
                {
                    AutosId = table.Column<int>(type: "int", nullable: false),
                    PersonasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoPersona", x => new { x.AutosId, x.PersonasId });
                    table.ForeignKey(
                        name: "FK_AutoPersona_Autos_AutosId",
                        column: x => x.AutosId,
                        principalTable: "Autos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AutoPersona_Personas_PersonasId",
                        column: x => x.PersonasId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AutoPersona_PersonasId",
                table: "AutoPersona",
                column: "PersonasId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AutoPersona");

            migrationBuilder.AddColumn<int>(
                name: "PersonaId",
                table: "Autos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Autos_PersonaId",
                table: "Autos",
                column: "PersonaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Autos_Personas_PersonaId",
                table: "Autos",
                column: "PersonaId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
