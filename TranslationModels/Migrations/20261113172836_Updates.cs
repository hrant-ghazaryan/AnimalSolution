using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TranslationModels.Migrations
{
    /// <inheritdoc />
    public partial class Updates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EnumType",
                table: "Translations",
                newName: "EnumValuee");

            migrationBuilder.RenameColumn(
                name: "EnumValue",
                table: "Translations",
                newName: "EnumTypee");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
