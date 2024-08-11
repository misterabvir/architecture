using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    project_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_project", x => x.project_id);
                });

            migrationBuilder.CreateTable(
                name: "models",
                columns: table => new
                {
                    model_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    project_id = table.Column<int>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_models", x => x.model_id);
                    table.ForeignKey(
                        name: "fk_models_projects_project_id",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "project_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "settings",
                columns: table => new
                {
                    setting_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    project_id = table.Column<int>(type: "INTEGER", nullable: false),
                    parameter = table.Column<string>(type: "TEXT", nullable: false),
                    value = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_settings", x => x.setting_id);
                    table.ForeignKey(
                        name: "fk_settings_projects_project_id",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "project_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "textures",
                columns: table => new
                {
                    texture_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    model_id = table.Column<int>(type: "INTEGER", nullable: false),
                    pattern = table.Column<string>(type: "TEXT", nullable: false),
                    color = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_textures", x => x.texture_id);
                    table.ForeignKey(
                        name: "fk_textures_models_model_id",
                        column: x => x.model_id,
                        principalTable: "models",
                        principalColumn: "model_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "projects",
                columns: new[] { "project_id", "name" },
                values: new object[,]
                {
                    { 1, "Project Alpha" },
                    { 2, "Project Beta" },
                    { 3, "Project Delta" }
                });

            migrationBuilder.InsertData(
                table: "models",
                columns: new[] { "model_id", "name", "project_id" },
                values: new object[,]
                {
                    { 1, "Box", 1 },
                    { 2, "Sphere", 1 },
                    { 3, "Cylinder", 1 },
                    { 4, "Cylinder1", 2 },
                    { 5, "Cylinder2", 2 },
                    { 6, "Sphere", 2 },
                    { 7, "Box1", 3 },
                    { 8, "Box2", 3 }
                });

            migrationBuilder.InsertData(
                table: "settings",
                columns: new[] { "setting_id", "parameter", "project_id", "value" },
                values: new object[,]
                {
                    { 1, "Resolution", 1, "FullHd" },
                    { 2, "Environment", 1, "DevelopMode" },
                    { 3, "Resolution", 2, "2K" },
                    { 4, "Resolution", 3, "1080" },
                    { 5, "Environment", 3, "Production" }
                });

            migrationBuilder.InsertData(
                table: "textures",
                columns: new[] { "texture_id", "color", "model_id", "pattern" },
                values: new object[,]
                {
                    { 1, "Black", 1, "Solid" },
                    { 2, "Red", 1, "Stripes" },
                    { 3, "White", 2, "Solid" },
                    { 4, "Blue", 3, "Solid" },
                    { 5, "Yellow", 4, "Solid" },
                    { 6, "Green", 4, "Stripes" },
                    { 7, "Black", 5, "Solid" },
                    { 8, "White", 6, "Solid" },
                    { 9, "Red", 7, "Solid" },
                    { 10, "Blue", 8, "Solid" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_models_project_id",
                table: "models",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "ix_settings_project_id",
                table: "settings",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "ix_textures_model_id",
                table: "textures",
                column: "model_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "settings");

            migrationBuilder.DropTable(
                name: "textures");

            migrationBuilder.DropTable(
                name: "models");

            migrationBuilder.DropTable(
                name: "projects");
        }
    }
}
