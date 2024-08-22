using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CloudService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenamingEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "configurations",
                schema: "cloud");

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "cpu_configuration",
                keyColumn: "cpu_id",
                keyValue: new Guid("2b8ff258-4c77-4083-8f61-90d163fba5f9"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "cpu_configuration",
                keyColumn: "cpu_id",
                keyValue: new Guid("4b8b40e1-3fc8-4fa2-b5a7-c4f074b40fe3"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "cpu_configuration",
                keyColumn: "cpu_id",
                keyValue: new Guid("8485ca24-7d9e-49f2-bbfe-ecf9a07b06b1"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "cpu_configuration",
                keyColumn: "cpu_id",
                keyValue: new Guid("9e1028d3-9883-4153-ab09-4057559fab4b"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "ip_configuration",
                keyColumn: "ip_id",
                keyValue: new Guid("ed639c26-a960-4cee-88e0-a2f0106fb46e"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "ip_configuration",
                keyColumn: "ip_id",
                keyValue: new Guid("eeba3582-8e8a-46e5-a4dc-5885236b9e4b"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "os_configuration",
                keyColumn: "os_id",
                keyValue: new Guid("2b0eee2d-38b5-4f2e-9edc-b785f9ecd994"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "os_configuration",
                keyColumn: "os_id",
                keyValue: new Guid("77333839-40d1-4eb9-a98f-16409b110dfb"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "os_configuration",
                keyColumn: "os_id",
                keyValue: new Guid("9adcb21d-8084-45c8-92e6-54d57128d7a6"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "os_configuration",
                keyColumn: "os_id",
                keyValue: new Guid("f42ecf13-658e-49ae-9073-e5fec96e6022"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "ram_configuration",
                keyColumn: "ram_id",
                keyValue: new Guid("47cc8291-d7cd-4aff-97bd-f80b02151866"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "ram_configuration",
                keyColumn: "ram_id",
                keyValue: new Guid("a184c92b-54cf-4dfe-a9c7-8bca2c514b3e"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "ram_configuration",
                keyColumn: "ram_id",
                keyValue: new Guid("aba7e469-792e-4019-bb2b-ecf45abd5514"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "ram_configuration",
                keyColumn: "ram_id",
                keyValue: new Guid("eae1be23-b006-4bf3-a649-a24e6f0e2889"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "rom_configuration",
                keyColumn: "rom_id",
                keyValue: new Guid("29c0b899-c4a6-44c5-a8eb-a7d469a0613f"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "rom_configuration",
                keyColumn: "rom_id",
                keyValue: new Guid("7517d1d0-7fe4-4f9b-a18a-32deb253117e"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "rom_configuration",
                keyColumn: "rom_id",
                keyValue: new Guid("821da0ef-3f9b-4648-b4da-2565c3968f6e"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "rom_configuration",
                keyColumn: "rom_id",
                keyValue: new Guid("aeef7042-3664-4796-9287-87a61ebeb302"));

            migrationBuilder.CreateTable(
                name: "setups",
                schema: "cloud",
                columns: table => new
                {
                    setup_id = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("pk_setups", x => x.setup_id);
                    table.ForeignKey(
                        name: "fk_setups_cpu_configuration_cpu_id",
                        column: x => x.cpu_id,
                        principalSchema: "cloud",
                        principalTable: "cpu_configuration",
                        principalColumn: "cpu_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_setups_ip_configuration_ip_id",
                        column: x => x.ip_id,
                        principalSchema: "cloud",
                        principalTable: "ip_configuration",
                        principalColumn: "ip_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_setups_os_configuration_os_id",
                        column: x => x.os_id,
                        principalSchema: "cloud",
                        principalTable: "os_configuration",
                        principalColumn: "os_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_setups_ram_configuration_ram_id",
                        column: x => x.ram_id,
                        principalSchema: "cloud",
                        principalTable: "ram_configuration",
                        principalColumn: "ram_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_setups_rom_configuration_rom_id",
                        column: x => x.rom_id,
                        principalSchema: "cloud",
                        principalTable: "rom_configuration",
                        principalColumn: "rom_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_setups_users_setup_id",
                        column: x => x.setup_id,
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
                    { new Guid("57dca4d4-09e7-4170-9715-2482018c92d2"), "4 vCPUs", 40.00m },
                    { new Guid("b7581b57-44bc-48ef-b3d4-544a7cf48585"), "2 vCPUs", 20.00m },
                    { new Guid("c5ad3c89-d1f4-4817-8749-369aa257f59e"), "16 vCPUs", 160.00m },
                    { new Guid("e10a1280-faad-4030-a31f-c482d3c0dadd"), "8 vCPUs", 80.00m }
                });

            migrationBuilder.InsertData(
                schema: "cloud",
                table: "ip_configuration",
                columns: new[] { "ip_id", "name", "price" },
                values: new object[,]
                {
                    { new Guid("a0a70acc-7ec3-47c7-993c-109ed3d35bbd"), "Dynamic IP", 10.00m },
                    { new Guid("dacca587-4ddc-47fb-89db-c1d3f0a010c8"), "Static IP", 20.00m }
                });

            migrationBuilder.InsertData(
                schema: "cloud",
                table: "os_configuration",
                columns: new[] { "os_id", "name", "price" },
                values: new object[,]
                {
                    { new Guid("2e6e3ffb-9a82-4386-bcb3-08953b8626b5"), "Windows Server", 50.00m },
                    { new Guid("37415fa4-caf8-4958-a211-0276949f782c"), "Windows 10", 30.00m },
                    { new Guid("a4c9838f-dfc6-44b0-af7c-fd8b559a9b31"), "Linux Ubuntu", 0.00m },
                    { new Guid("fe2c8c91-6699-4110-82a4-c0a08cbc810e"), "Red Hat Enterprise Linux", 25.00m }
                });

            migrationBuilder.InsertData(
                schema: "cloud",
                table: "ram_configuration",
                columns: new[] { "ram_id", "name", "price" },
                values: new object[,]
                {
                    { new Guid("2ca7c632-8e0f-4396-b0be-ef2ebd12f6f1"), "16 GB", 240.00m },
                    { new Guid("75a5f76f-1ddd-404c-ad2f-885afff1a564"), "4 GB", 60.00m },
                    { new Guid("98519650-db5e-48e7-9458-aeed4a8dd369"), "2 GB", 30.00m },
                    { new Guid("a191fe20-5c06-4859-bb9b-3f90e715834d"), "8 GB", 120.00m }
                });

            migrationBuilder.InsertData(
                schema: "cloud",
                table: "rom_configuration",
                columns: new[] { "rom_id", "name", "price" },
                values: new object[,]
                {
                    { new Guid("2f169751-c688-4201-a02b-7540852c0ddf"), "1 TB", 400.00m },
                    { new Guid("d1c9103d-e37c-4533-83e1-2bfd39b865a0"), "512 GB", 200.00m },
                    { new Guid("d3404526-45ea-4676-b109-dbc0b0a7aec0"), "128 GB", 50.00m },
                    { new Guid("fe5d4d89-0040-46c0-b0ff-81c765585a0c"), "256 GB", 100.00m }
                });

            migrationBuilder.CreateIndex(
                name: "ix_setups_cpu_id",
                schema: "cloud",
                table: "setups",
                column: "cpu_id");

            migrationBuilder.CreateIndex(
                name: "ix_setups_ip_id",
                schema: "cloud",
                table: "setups",
                column: "ip_id");

            migrationBuilder.CreateIndex(
                name: "ix_setups_os_id",
                schema: "cloud",
                table: "setups",
                column: "os_id");

            migrationBuilder.CreateIndex(
                name: "ix_setups_ram_id",
                schema: "cloud",
                table: "setups",
                column: "ram_id");

            migrationBuilder.CreateIndex(
                name: "ix_setups_rom_id",
                schema: "cloud",
                table: "setups",
                column: "rom_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "setups",
                schema: "cloud");

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "cpu_configuration",
                keyColumn: "cpu_id",
                keyValue: new Guid("57dca4d4-09e7-4170-9715-2482018c92d2"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "cpu_configuration",
                keyColumn: "cpu_id",
                keyValue: new Guid("b7581b57-44bc-48ef-b3d4-544a7cf48585"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "cpu_configuration",
                keyColumn: "cpu_id",
                keyValue: new Guid("c5ad3c89-d1f4-4817-8749-369aa257f59e"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "cpu_configuration",
                keyColumn: "cpu_id",
                keyValue: new Guid("e10a1280-faad-4030-a31f-c482d3c0dadd"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "ip_configuration",
                keyColumn: "ip_id",
                keyValue: new Guid("a0a70acc-7ec3-47c7-993c-109ed3d35bbd"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "ip_configuration",
                keyColumn: "ip_id",
                keyValue: new Guid("dacca587-4ddc-47fb-89db-c1d3f0a010c8"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "os_configuration",
                keyColumn: "os_id",
                keyValue: new Guid("2e6e3ffb-9a82-4386-bcb3-08953b8626b5"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "os_configuration",
                keyColumn: "os_id",
                keyValue: new Guid("37415fa4-caf8-4958-a211-0276949f782c"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "os_configuration",
                keyColumn: "os_id",
                keyValue: new Guid("a4c9838f-dfc6-44b0-af7c-fd8b559a9b31"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "os_configuration",
                keyColumn: "os_id",
                keyValue: new Guid("fe2c8c91-6699-4110-82a4-c0a08cbc810e"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "ram_configuration",
                keyColumn: "ram_id",
                keyValue: new Guid("2ca7c632-8e0f-4396-b0be-ef2ebd12f6f1"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "ram_configuration",
                keyColumn: "ram_id",
                keyValue: new Guid("75a5f76f-1ddd-404c-ad2f-885afff1a564"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "ram_configuration",
                keyColumn: "ram_id",
                keyValue: new Guid("98519650-db5e-48e7-9458-aeed4a8dd369"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "ram_configuration",
                keyColumn: "ram_id",
                keyValue: new Guid("a191fe20-5c06-4859-bb9b-3f90e715834d"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "rom_configuration",
                keyColumn: "rom_id",
                keyValue: new Guid("2f169751-c688-4201-a02b-7540852c0ddf"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "rom_configuration",
                keyColumn: "rom_id",
                keyValue: new Guid("d1c9103d-e37c-4533-83e1-2bfd39b865a0"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "rom_configuration",
                keyColumn: "rom_id",
                keyValue: new Guid("d3404526-45ea-4676-b109-dbc0b0a7aec0"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "rom_configuration",
                keyColumn: "rom_id",
                keyValue: new Guid("fe5d4d89-0040-46c0-b0ff-81c765585a0c"));

            migrationBuilder.CreateTable(
                name: "configurations",
                schema: "cloud",
                columns: table => new
                {
                    configuration_id = table.Column<Guid>(type: "uuid", nullable: false),
                    cpu_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ip_id = table.Column<Guid>(type: "uuid", nullable: false),
                    os_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ram_id = table.Column<Guid>(type: "uuid", nullable: false),
                    rom_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<string>(type: "character varying(31)", maxLength: 31, nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
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
        }
    }
}
