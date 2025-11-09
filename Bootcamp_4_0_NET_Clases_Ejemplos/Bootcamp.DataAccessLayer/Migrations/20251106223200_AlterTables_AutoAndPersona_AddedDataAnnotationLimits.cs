using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bootcamp.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AlterTables_AutoAndPersona_AddedDataAnnotationLimits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Patente",
                table: "Autos",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Patente",
                table: "Autos");
        }
    }
}
