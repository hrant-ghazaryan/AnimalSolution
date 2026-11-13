using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TranslationModels.Migrations
{
    /// <inheritdoc />
    public partial class Translation_Entity_Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Translations_ResourceType_ResourceCode_LanguageCode",
                table: "Translations");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Translations",
                newName: "TranslatedText");

            migrationBuilder.RenameColumn(
                name: "ResourceType",
                table: "Translations",
                newName: "EnumType");

            migrationBuilder.RenameColumn(
                name: "ResourceCode",
                table: "Translations",
                newName: "EnumValue");

            migrationBuilder.CreateIndex(
                name: "IX_Translations_EnumType_EnumValue_LanguageCode",
                table: "Translations",
                columns: new[] { "EnumType", "EnumValue", "LanguageCode" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Translations_EnumType_EnumValue_LanguageCode",
                table: "Translations");

            migrationBuilder.RenameColumn(
                name: "TranslatedText",
                table: "Translations",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "EnumValue",
                table: "Translations",
                newName: "ResourceType");

            migrationBuilder.RenameColumn(
                name: "EnumType",
                table: "Translations",
                newName: "ResourceCode");

            migrationBuilder.CreateIndex(
                name: "IX_Translations_ResourceType_ResourceCode_LanguageCode",
                table: "Translations",
                columns: new[] { "ResourceType", "ResourceCode", "LanguageCode" },
                unique: true);
        }
    }
}
