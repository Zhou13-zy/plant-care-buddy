using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlantCareBuddy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBeforeAfterImageToCE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "CareEvents",
                newName: "BeforeImagePath");

            migrationBuilder.AddColumn<string>(
                name: "AfterImagePath",
                table: "CareEvents",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AfterImagePath",
                table: "CareEvents");

            migrationBuilder.RenameColumn(
                name: "BeforeImagePath",
                table: "CareEvents",
                newName: "ImagePath");
        }
    }
}
