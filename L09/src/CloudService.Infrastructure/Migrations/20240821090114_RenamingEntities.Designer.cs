﻿// <auto-generated />
using System;
using CloudService.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CloudService.Infrastructure.Migrations
{
    [DbContext(typeof(CloudServiceDbContext))]
    [Migration("20240821090114_RenamingEntities")]
    partial class RenamingEntities
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("cloud")
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CloudService.Domain.Cpu", b =>
                {
                    b.Property<Guid>("CpuId")
                        .HasColumnType("uuid")
                        .HasColumnName("cpu_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("price");

                    b.HasKey("CpuId")
                        .HasName("pk_cpu");

                    b.ToTable("cpu_configuration", "cloud");

                    b.HasData(
                        new
                        {
                            CpuId = new Guid("b7581b57-44bc-48ef-b3d4-544a7cf48585"),
                            Name = "2 vCPUs",
                            Price = 20.00m
                        },
                        new
                        {
                            CpuId = new Guid("57dca4d4-09e7-4170-9715-2482018c92d2"),
                            Name = "4 vCPUs",
                            Price = 40.00m
                        },
                        new
                        {
                            CpuId = new Guid("e10a1280-faad-4030-a31f-c482d3c0dadd"),
                            Name = "8 vCPUs",
                            Price = 80.00m
                        },
                        new
                        {
                            CpuId = new Guid("c5ad3c89-d1f4-4817-8749-369aa257f59e"),
                            Name = "16 vCPUs",
                            Price = 160.00m
                        });
                });

            modelBuilder.Entity("CloudService.Domain.Ip", b =>
                {
                    b.Property<Guid>("IpId")
                        .HasColumnType("uuid")
                        .HasColumnName("ip_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("price");

                    b.HasKey("IpId")
                        .HasName("pk_ip");

                    b.ToTable("ip_configuration", "cloud");

                    b.HasData(
                        new
                        {
                            IpId = new Guid("a0a70acc-7ec3-47c7-993c-109ed3d35bbd"),
                            Name = "Dynamic IP",
                            Price = 10.00m
                        },
                        new
                        {
                            IpId = new Guid("dacca587-4ddc-47fb-89db-c1d3f0a010c8"),
                            Name = "Static IP",
                            Price = 20.00m
                        });
                });

            modelBuilder.Entity("CloudService.Domain.Os", b =>
                {
                    b.Property<Guid>("OsId")
                        .HasColumnType("uuid")
                        .HasColumnName("os_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("price");

                    b.HasKey("OsId")
                        .HasName("pk_os");

                    b.ToTable("os_configuration", "cloud");

                    b.HasData(
                        new
                        {
                            OsId = new Guid("a4c9838f-dfc6-44b0-af7c-fd8b559a9b31"),
                            Name = "Linux Ubuntu",
                            Price = 0.00m
                        },
                        new
                        {
                            OsId = new Guid("37415fa4-caf8-4958-a211-0276949f782c"),
                            Name = "Windows 10",
                            Price = 30.00m
                        },
                        new
                        {
                            OsId = new Guid("2e6e3ffb-9a82-4386-bcb3-08953b8626b5"),
                            Name = "Windows Server",
                            Price = 50.00m
                        },
                        new
                        {
                            OsId = new Guid("fe2c8c91-6699-4110-82a4-c0a08cbc810e"),
                            Name = "Red Hat Enterprise Linux",
                            Price = 25.00m
                        });
                });

            modelBuilder.Entity("CloudService.Domain.Ram", b =>
                {
                    b.Property<Guid>("RamId")
                        .HasColumnType("uuid")
                        .HasColumnName("ram_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("price");

                    b.HasKey("RamId")
                        .HasName("pk_ram");

                    b.ToTable("ram_configuration", "cloud");

                    b.HasData(
                        new
                        {
                            RamId = new Guid("98519650-db5e-48e7-9458-aeed4a8dd369"),
                            Name = "2 GB",
                            Price = 30.00m
                        },
                        new
                        {
                            RamId = new Guid("75a5f76f-1ddd-404c-ad2f-885afff1a564"),
                            Name = "4 GB",
                            Price = 60.00m
                        },
                        new
                        {
                            RamId = new Guid("a191fe20-5c06-4859-bb9b-3f90e715834d"),
                            Name = "8 GB",
                            Price = 120.00m
                        },
                        new
                        {
                            RamId = new Guid("2ca7c632-8e0f-4396-b0be-ef2ebd12f6f1"),
                            Name = "16 GB",
                            Price = 240.00m
                        });
                });

            modelBuilder.Entity("CloudService.Domain.Rom", b =>
                {
                    b.Property<Guid>("RomId")
                        .HasColumnType("uuid")
                        .HasColumnName("rom_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("price");

                    b.HasKey("RomId")
                        .HasName("pk_rom");

                    b.ToTable("rom_configuration", "cloud");

                    b.HasData(
                        new
                        {
                            RomId = new Guid("d3404526-45ea-4676-b109-dbc0b0a7aec0"),
                            Name = "128 GB",
                            Price = 50.00m
                        },
                        new
                        {
                            RomId = new Guid("fe5d4d89-0040-46c0-b0ff-81c765585a0c"),
                            Name = "256 GB",
                            Price = 100.00m
                        },
                        new
                        {
                            RomId = new Guid("d1c9103d-e37c-4533-83e1-2bfd39b865a0"),
                            Name = "512 GB",
                            Price = 200.00m
                        },
                        new
                        {
                            RomId = new Guid("2f169751-c688-4201-a02b-7540852c0ddf"),
                            Name = "1 TB",
                            Price = 400.00m
                        });
                });

            modelBuilder.Entity("CloudService.Domain.Setup", b =>
                {
                    b.Property<Guid>("SetupId")
                        .HasColumnType("uuid")
                        .HasColumnName("setup_id");

                    b.Property<Guid>("CpuId")
                        .HasColumnType("uuid")
                        .HasColumnName("cpu_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<Guid>("IpId")
                        .HasColumnType("uuid")
                        .HasColumnName("ip_id");

                    b.Property<Guid>("OsId")
                        .HasColumnType("uuid")
                        .HasColumnName("os_id");

                    b.Property<Guid>("RamId")
                        .HasColumnType("uuid")
                        .HasColumnName("ram_id");

                    b.Property<Guid>("RomId")
                        .HasColumnType("uuid")
                        .HasColumnName("rom_id");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(31)
                        .HasColumnType("character varying(31)")
                        .HasColumnName("status");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("SetupId")
                        .HasName("pk_setups");

                    b.HasIndex("CpuId")
                        .HasDatabaseName("ix_setups_cpu_id");

                    b.HasIndex("IpId")
                        .HasDatabaseName("ix_setups_ip_id");

                    b.HasIndex("OsId")
                        .HasDatabaseName("ix_setups_os_id");

                    b.HasIndex("RamId")
                        .HasDatabaseName("ix_setups_ram_id");

                    b.HasIndex("RomId")
                        .HasDatabaseName("ix_setups_rom_id");

                    b.ToTable("setups", "cloud");
                });

            modelBuilder.Entity("CloudService.Domain.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("password");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("username");

                    b.HasKey("UserId")
                        .HasName("pk_users");

                    b.HasIndex(new[] { "UserId" }, "idx_users_id")
                        .IsUnique()
                        .HasDatabaseName("ix_users_user_id");

                    b.HasIndex(new[] { "Username" }, "idx_users_username")
                        .IsUnique()
                        .HasDatabaseName("ix_users_username");

                    b.ToTable("users", "cloud");
                });

            modelBuilder.Entity("CloudService.Domain.Setup", b =>
                {
                    b.HasOne("CloudService.Domain.Cpu", "Cpu")
                        .WithMany()
                        .HasForeignKey("CpuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_setups_cpu_configuration_cpu_id");

                    b.HasOne("CloudService.Domain.Ip", "Ip")
                        .WithMany()
                        .HasForeignKey("IpId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_setups_ip_configuration_ip_id");

                    b.HasOne("CloudService.Domain.Os", "Os")
                        .WithMany()
                        .HasForeignKey("OsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_setups_os_configuration_os_id");

                    b.HasOne("CloudService.Domain.Ram", "Ram")
                        .WithMany()
                        .HasForeignKey("RamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_setups_ram_configuration_ram_id");

                    b.HasOne("CloudService.Domain.Rom", "Rom")
                        .WithMany()
                        .HasForeignKey("RomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_setups_rom_configuration_rom_id");

                    b.HasOne("CloudService.Domain.User", null)
                        .WithMany("Configs")
                        .HasForeignKey("SetupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_setups_users_setup_id");

                    b.Navigation("Cpu");

                    b.Navigation("Ip");

                    b.Navigation("Os");

                    b.Navigation("Ram");

                    b.Navigation("Rom");
                });

            modelBuilder.Entity("CloudService.Domain.User", b =>
                {
                    b.Navigation("Configs");
                });
#pragma warning restore 612, 618
        }
    }
}
