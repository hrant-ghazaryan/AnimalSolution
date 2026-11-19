using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalModels.Migrations
{
    /// <inheritdoc />
    public partial class AnimalsTable_NewColumn_OwnedBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnedBy",
                table: "Animals",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnedBy",
                table: "Animals");
        }
    }
}
