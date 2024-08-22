using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CloudService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixBug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_setups_users_setup_id",
                schema: "cloud",
                table: "setups");

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

            migrationBuilder.InsertData(
                schema: "cloud",
                table: "cpu_configuration",
                columns: new[] { "cpu_id", "name", "price" },
                values: new object[,]
                {
                    { new Guid("1e4b235c-364f-4d4b-b42d-60aaddbcc7ef"), "4 vCPUs", 40.00m },
                    { new Guid("1f353430-0c15-4af9-ad13-6122275a1636"), "8 vCPUs", 80.00m },
                    { new Guid("5936138d-2118-4425-a199-d57d587382ee"), "2 vCPUs", 20.00m },
                    { new Guid("cc6cf7c8-8945-4586-89ad-828277e39a1b"), "16 vCPUs", 160.00m }
                });

            migrationBuilder.InsertData(
                schema: "cloud",
                table: "ip_configuration",
                columns: new[] { "ip_id", "name", "price" },
                values: new object[,]
                {
                    { new Guid("0c962387-6b55-4e12-8f7f-84f9cf5636e5"), "Dynamic IP", 10.00m },
                    { new Guid("9b5ee8c4-bb5c-4181-8fb9-f05f5087b2fd"), "Static IP", 20.00m }
                });

            migrationBuilder.InsertData(
                schema: "cloud",
                table: "os_configuration",
                columns: new[] { "os_id", "name", "price" },
                values: new object[,]
                {
                    { new Guid("7991a3c5-adf6-4f55-b5fc-17671ea6a545"), "Windows 10", 30.00m },
                    { new Guid("e903b466-7d93-4f73-be85-f3f7098480f6"), "Red Hat Enterprise Linux", 25.00m },
                    { new Guid("f5306e60-4c52-4579-8d4e-0eb350be4433"), "Linux Ubuntu", 0.00m },
                    { new Guid("fa451cd0-2140-4b9f-8e82-0bc0299214ae"), "Windows Server", 50.00m }
                });

            migrationBuilder.InsertData(
                schema: "cloud",
                table: "ram_configuration",
                columns: new[] { "ram_id", "name", "price" },
                values: new object[,]
                {
                    { new Guid("2ae9780b-ddf6-49cb-8162-2a540d17f5a3"), "4 GB", 60.00m },
                    { new Guid("7483ac57-c3d0-4705-b974-9cb97bd9c3ec"), "2 GB", 30.00m },
                    { new Guid("8d865fee-f704-490a-81a1-bc82a81fe12b"), "8 GB", 120.00m },
                    { new Guid("cc0f8905-e488-463e-a609-dbe1d0a0a67e"), "16 GB", 240.00m }
                });

            migrationBuilder.InsertData(
                schema: "cloud",
                table: "rom_configuration",
                columns: new[] { "rom_id", "name", "price" },
                values: new object[,]
                {
                    { new Guid("54d95712-3b02-4bf4-9af4-1ef627680b02"), "128 GB", 50.00m },
                    { new Guid("573a9c1f-5400-4e6c-9573-4766db55fd2d"), "1 TB", 400.00m },
                    { new Guid("74e656d7-5b28-4d63-b335-0caf9833b2a6"), "512 GB", 200.00m },
                    { new Guid("c4852627-d7ff-41c0-bb2e-0e0797e3fe1c"), "256 GB", 100.00m }
                });

            migrationBuilder.CreateIndex(
                name: "ix_setups_user_id",
                schema: "cloud",
                table: "setups",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_setups_users_user_id",
                schema: "cloud",
                table: "setups",
                column: "user_id",
                principalSchema: "cloud",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_setups_users_user_id",
                schema: "cloud",
                table: "setups");

            migrationBuilder.DropIndex(
                name: "ix_setups_user_id",
                schema: "cloud",
                table: "setups");

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "cpu_configuration",
                keyColumn: "cpu_id",
                keyValue: new Guid("1e4b235c-364f-4d4b-b42d-60aaddbcc7ef"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "cpu_configuration",
                keyColumn: "cpu_id",
                keyValue: new Guid("1f353430-0c15-4af9-ad13-6122275a1636"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "cpu_configuration",
                keyColumn: "cpu_id",
                keyValue: new Guid("5936138d-2118-4425-a199-d57d587382ee"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "cpu_configuration",
                keyColumn: "cpu_id",
                keyValue: new Guid("cc6cf7c8-8945-4586-89ad-828277e39a1b"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "ip_configuration",
                keyColumn: "ip_id",
                keyValue: new Guid("0c962387-6b55-4e12-8f7f-84f9cf5636e5"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "ip_configuration",
                keyColumn: "ip_id",
                keyValue: new Guid("9b5ee8c4-bb5c-4181-8fb9-f05f5087b2fd"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "os_configuration",
                keyColumn: "os_id",
                keyValue: new Guid("7991a3c5-adf6-4f55-b5fc-17671ea6a545"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "os_configuration",
                keyColumn: "os_id",
                keyValue: new Guid("e903b466-7d93-4f73-be85-f3f7098480f6"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "os_configuration",
                keyColumn: "os_id",
                keyValue: new Guid("f5306e60-4c52-4579-8d4e-0eb350be4433"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "os_configuration",
                keyColumn: "os_id",
                keyValue: new Guid("fa451cd0-2140-4b9f-8e82-0bc0299214ae"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "ram_configuration",
                keyColumn: "ram_id",
                keyValue: new Guid("2ae9780b-ddf6-49cb-8162-2a540d17f5a3"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "ram_configuration",
                keyColumn: "ram_id",
                keyValue: new Guid("7483ac57-c3d0-4705-b974-9cb97bd9c3ec"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "ram_configuration",
                keyColumn: "ram_id",
                keyValue: new Guid("8d865fee-f704-490a-81a1-bc82a81fe12b"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "ram_configuration",
                keyColumn: "ram_id",
                keyValue: new Guid("cc0f8905-e488-463e-a609-dbe1d0a0a67e"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "rom_configuration",
                keyColumn: "rom_id",
                keyValue: new Guid("54d95712-3b02-4bf4-9af4-1ef627680b02"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "rom_configuration",
                keyColumn: "rom_id",
                keyValue: new Guid("573a9c1f-5400-4e6c-9573-4766db55fd2d"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "rom_configuration",
                keyColumn: "rom_id",
                keyValue: new Guid("74e656d7-5b28-4d63-b335-0caf9833b2a6"));

            migrationBuilder.DeleteData(
                schema: "cloud",
                table: "rom_configuration",
                keyColumn: "rom_id",
                keyValue: new Guid("c4852627-d7ff-41c0-bb2e-0e0797e3fe1c"));

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

            migrationBuilder.AddForeignKey(
                name: "fk_setups_users_setup_id",
                schema: "cloud",
                table: "setups",
                column: "setup_id",
                principalSchema: "cloud",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
