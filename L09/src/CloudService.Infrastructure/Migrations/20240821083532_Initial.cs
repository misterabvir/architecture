using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CloudService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "cloud");

            migrationBuilder.CreateTable(
                name: "cpu_configuration",
                schema: "cloud",
                columns: table => new
                {
                    cpu_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cpu", x => x.cpu_id);
                });

            migrationBuilder.CreateTable(
                name: "ip_configuration",
                schema: "cloud",
                columns: table => new
                {
                    ip_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ip", x => x.ip_id);
                });

            migrationBuilder.CreateTable(
                name: "os_configuration",
                schema: "cloud",
                columns: table => new
                {
                    os_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_os", x => x.os_id);
                });

            migrationBuilder.CreateTable(
                name: "ram_configuration",
                schema: "cloud",
                columns: table => new
                {
                    ram_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ram", x => x.ram_id);
                });

            migrationBuilder.CreateTable(
                name: "rom_configuration",
                schema: "cloud",
                columns: table => new
                {
                    rom_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rom", x => x.rom_id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "cloud",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "configurations",
                schema: "cloud",
                columns: table => new
                {
                    configuration_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    cpu_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ram_id = table.Column<Guid>(type: "uuid", nullable: false),
                    rom_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ip_id = table.Column<Guid>(type: "uuid", nullable: false),
                    os_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<string>(type: "character varying(31)", maxLength: 31, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_configurations", x => x.configuration_id);
                    table.ForeignKey(
                        name: "fk_configurations_cpus_cpu_id",
                        column: x => x.cpu_id,
                        principalSchema: "cloud",
                        principalTable: "cpu_configuration",
                        principalColumn: "cpu_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_configurations_ips_ip_id",
                        column: x => x.ip_id,
                        principalSchema: "cloud",
                        principalTable: "ip_configuration",
                        principalColumn: "ip_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_configurations_oss_os_id",
                        column: x => x.os_id,
                        principalSchema: "cloud",
                        principalTable: "os_configuration",
                        principalColumn: "os_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_configurations_rams_ram_id",
                        column: x => x.ram_id,
                        principalSchema: "cloud",
                        principalTable: "ram_configuration",
                        principalColumn: "ram_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_configurations_roms_rom_id",
                        column: x => x.rom_id,
                        principalSchema: "cloud",
                        principalTable: "rom_configuration",
                        principalColumn: "rom_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_configurations_users_configuration_id",
                        column: x => x.configuration_id,
                        principalSchema: "cloud",
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "cloud",
                table: "cpu_configuration",
                columns: new[] { "cpu_id", "name", "price" },
                values: new object[,]
                {
                    { new Guid("2b8ff258-4c77-4083-8f61-90d163fba5f9"), "8 vCPUs", 80.00m },
                    { new Guid("4b8b40e1-3fc8-4fa2-b5a7-c4f074b40fe3"), "4 vCPUs", 40.00m },
                    { new Guid("8485ca24-7d9e-49f2-bbfe-ecf9a07b06b1"), "2 vCPUs", 20.00m },
                    { new Guid("9e1028d3-9883-4153-ab09-4057559fab4b"), "16 vCPUs", 160.00m }
                });

            migrationBuilder.InsertData(
                schema: "cloud",
                table: "ip_configuration",
                columns: new[] { "ip_id", "name", "price" },
                values: new object[,]
                {
                    { new Guid("ed639c26-a960-4cee-88e0-a2f0106fb46e"), "Dynamic IP", 10.00m },
                    { new Guid("eeba3582-8e8a-46e5-a4dc-5885236b9e4b"), "Static IP", 20.00m }
                });

            migrationBuilder.InsertData(
                schema: "cloud",
                table: "os_configuration",
                columns: new[] { "os_id", "name", "price" },
                values: new object[,]
                {
                    { new Guid("2b0eee2d-38b5-4f2e-9edc-b785f9ecd994"), "Linux Ubuntu", 0.00m },
                    { new Guid("77333839-40d1-4eb9-a98f-16409b110dfb"), "Windows 10", 30.00m },
                    { new Guid("9adcb21d-8084-45c8-92e6-54d57128d7a6"), "Windows Server", 50.00m },
                    { new Guid("f42ecf13-658e-49ae-9073-e5fec96e6022"), "Red Hat Enterprise Linux", 25.00m }
                });

            migrationBuilder.InsertData(
                schema: "cloud",
                table: "ram_configuration",
                columns: new[] { "ram_id", "name", "price" },
                values: new object[,]
                {
                    { new Guid("47cc8291-d7cd-4aff-97bd-f80b02151866"), "8 GB", 120.00m },
                    { new Guid("a184c92b-54cf-4dfe-a9c7-8bca2c514b3e"), "2 GB", 30.00m },
                    { new Guid("aba7e469-792e-4019-bb2b-ecf45abd5514"), "4 GB", 60.00m },
                    { new Guid("eae1be23-b006-4bf3-a649-a24e6f0e2889"), "16 GB", 240.00m }
                });

            migrationBuilder.InsertData(
                schema: "cloud",
                table: "rom_configuration",
                columns: new[] { "rom_id", "name", "price" },
                values: new object[,]
                {
                    { new Guid("29c0b899-c4a6-44c5-a8eb-a7d469a0613f"), "256 GB", 100.00m },
                    { new Guid("7517d1d0-7fe4-4f9b-a18a-32deb253117e"), "1 TB", 400.00m },
                    { new Guid("821da0ef-3f9b-4648-b4da-2565c3968f6e"), "128 GB", 50.00m },
                    { new Guid("aeef7042-3664-4796-9287-87a61ebeb302"), "512 GB", 200.00m }
                });

            migrationBuilder.CreateIndex(
                name: "ix_configurations_cpu_id",
                schema: "cloud",
                table: "configurations",
                column: "cpu_id");

            migrationBuilder.CreateIndex(
                name: "ix_configurations_ip_id",
                schema: "cloud",
                table: "configurations",
                column: "ip_id");

            migrationBuilder.CreateIndex(
                name: "ix_configurations_os_id",
                schema: "cloud",
                table: "configurations",
                column: "os_id");

            migrationBuilder.CreateIndex(
                name: "ix_configurations_ram_id",
                schema: "cloud",
                table: "configurations",
                column: "ram_id");

            migrationBuilder.CreateIndex(
                name: "ix_configurations_rom_id",
                schema: "cloud",
                table: "configurations",
                column: "rom_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_user_id",
                schema: "cloud",
                table: "users",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_username",
                schema: "cloud",
                table: "users",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "configurations",
                schema: "cloud");

            migrationBuilder.DropTable(
                name: "cpu_configuration",
                schema: "cloud");

            migrationBuilder.DropTable(
                name: "ip_configuration",
                schema: "cloud");

            migrationBuilder.DropTable(
                name: "os_configuration",
                schema: "cloud");

            migrationBuilder.DropTable(
                name: "ram_configuration",
                schema: "cloud");

            migrationBuilder.DropTable(
                name: "rom_configuration",
                schema: "cloud");

            migrationBuilder.DropTable(
                name: "users",
                schema: "cloud");
        }
    }
}
