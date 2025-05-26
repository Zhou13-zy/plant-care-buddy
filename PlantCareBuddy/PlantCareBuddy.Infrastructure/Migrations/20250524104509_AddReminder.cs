using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlantCareBuddy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddReminder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reminders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Recurrence_Type = table.Column<int>(type: "int", nullable: false),
                    Recurrence_Interval = table.Column<int>(type: "int", nullable: false),
                    Recurrence_EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recurrence_OccurrenceCount = table.Column<int>(type: "int", nullable: true),
                    Recurrence_DaysOfWeek = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Recurrence_DayOfMonth = table.Column<int>(type: "int", nullable: true),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    CompletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StrategyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StrategyParameters = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsStrategyOverride = table.Column<bool>(type: "bit", nullable: false),
                    LastStrategyAdjustment = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Intensity = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reminders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reminders_Plants_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_DueDate",
                table: "Reminders",
                column: "DueDate");

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_IsCompleted",
                table: "Reminders",
                column: "IsCompleted");

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_PlantId",
                table: "Reminders",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_StrategyId",
                table: "Reminders",
                column: "StrategyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reminders");
        }
    }
}
