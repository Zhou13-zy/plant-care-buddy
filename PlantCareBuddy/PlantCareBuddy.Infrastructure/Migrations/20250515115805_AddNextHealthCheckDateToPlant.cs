using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlantCareBuddy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNextHealthCheckDateToPlant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "NextHealthCheckDate",
                table: "Plants",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NextHealthCheckDate",
                table: "Plants");
        }
    }
}
