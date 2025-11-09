using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bootcamp.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AlterTable_AddedDniToPersona : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Dni",
                table: "Personas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dni",
                table: "Personas");
        }
    }
}
