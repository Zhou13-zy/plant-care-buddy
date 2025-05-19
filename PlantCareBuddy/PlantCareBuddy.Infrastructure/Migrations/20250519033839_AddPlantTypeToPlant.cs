using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlantCareBuddy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPlantTypeToPlant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlantType",
                table: "Plants",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlantType",
                table: "Plants");
        }
    }
}
