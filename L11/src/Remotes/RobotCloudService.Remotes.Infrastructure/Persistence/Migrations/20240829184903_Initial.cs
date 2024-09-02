using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RobotCloudService.Remotes.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "remotes");

            migrationBuilder.CreateTable(
                name: "users",
                schema: "remotes",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "logs",
                schema: "remotes",
                columns: table => new
                {
                    log_id = table.Column<string>(type: "text", nullable: false),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    message = table.Column<string>(type: "text", nullable: false),
                    occured_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_logs", x => new { x.user_id, x.log_id });
                    table.ForeignKey(
                        name: "FK_logs_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "remotes",
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "robots",
                schema: "remotes",
                columns: table => new
                {
                    robot_id = table.Column<string>(type: "text", nullable: false),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    model = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    speed = table.Column<double>(type: "double precision", maxLength: 100, nullable: false),
                    state = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    room_id = table.Column<string>(type: "text", nullable: false),
                    CalculatedTimeOfClaningOver = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_robots", x => new { x.user_id, x.robot_id });
                    table.ForeignKey(
                        name: "FK_robots_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "remotes",
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rooms",
                schema: "remotes",
                columns: table => new
                {
                    room_id = table.Column<string>(type: "text", nullable: false),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    square = table.Column<double>(type: "double precision", maxLength: 100, nullable: false),
                    last_cleaned_at = table.Column<DateTime>(type: "timestamp with time zone", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rooms", x => new { x.user_id, x.room_id });
                    table.ForeignKey(
                        name: "FK_rooms_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "remotes",
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "logs",
                schema: "remotes");

            migrationBuilder.DropTable(
                name: "robots",
                schema: "remotes");

            migrationBuilder.DropTable(
                name: "rooms",
                schema: "remotes");

            migrationBuilder.DropTable(
                name: "users",
                schema: "remotes");
        }
    }
}
