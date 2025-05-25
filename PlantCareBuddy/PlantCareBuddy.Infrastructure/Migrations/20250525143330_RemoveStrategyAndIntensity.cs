using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlantCareBuddy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveStrategyAndIntensity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reminders_StrategyId",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "Intensity",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "IsStrategyOverride",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "LastStrategyAdjustment",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "StrategyId",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "StrategyParameters",
                table: "Reminders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Intensity",
                table: "Reminders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsStrategyOverride",
                table: "Reminders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastStrategyAdjustment",
                table: "Reminders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StrategyId",
                table: "Reminders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StrategyParameters",
                table: "Reminders",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_StrategyId",
                table: "Reminders",
                column: "StrategyId");
        }
    }
}
