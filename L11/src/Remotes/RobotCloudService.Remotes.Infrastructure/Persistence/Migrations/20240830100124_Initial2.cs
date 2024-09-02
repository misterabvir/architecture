using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RobotCloudService.Remotes.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CalculatedTimeOfClaningOver",
                schema: "remotes",
                table: "robots",
                newName: "calculated_time_of_cleaning_over");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "calculated_time_of_cleaning_over",
                schema: "remotes",
                table: "robots",
                newName: "CalculatedTimeOfClaningOver");
        }
    }
}
