using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TranslationModels.Migrations
{
    /// <inheritdoc />
    public partial class TranslationsTableColumnNamesUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
               name: "EnumTypee",
               table: "Translations",
               newName: "EnumType");

            migrationBuilder.RenameColumn(
                name: "EnumValuee",
                table: "Translations",
                newName: "EnumValue");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
