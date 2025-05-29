using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlantCareBuddy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMonthOfYearToRecurrencePattern : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Recurrence_MonthOfYear",
                table: "Reminders",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Recurrence_MonthOfYear",
                table: "Reminders");
        }
    }
}
