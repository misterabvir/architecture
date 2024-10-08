﻿// <auto-generated />
using System;
using CloudService.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CloudService.Infrastructure.Migrations
{
    [DbContext(typeof(CloudServiceDbContext))]
    partial class CloudServiceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                            CpuId = new Guid("5936138d-2118-4425-a199-d57d587382ee"),
                            Name = "2 vCPUs",
                            Price = 20.00m
                        },
                        new
                        {
                            CpuId = new Guid("1e4b235c-364f-4d4b-b42d-60aaddbcc7ef"),
                            Name = "4 vCPUs",
                            Price = 40.00m
                        },
                        new
                        {
                            CpuId = new Guid("1f353430-0c15-4af9-ad13-6122275a1636"),
                            Name = "8 vCPUs",
                            Price = 80.00m
                        },
                        new
                        {
                            CpuId = new Guid("cc6cf7c8-8945-4586-89ad-828277e39a1b"),
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
                            IpId = new Guid("0c962387-6b55-4e12-8f7f-84f9cf5636e5"),
                            Name = "Dynamic IP",
                            Price = 10.00m
                        },
                        new
                        {
                            IpId = new Guid("9b5ee8c4-bb5c-4181-8fb9-f05f5087b2fd"),
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
                            OsId = new Guid("f5306e60-4c52-4579-8d4e-0eb350be4433"),
                            Name = "Linux Ubuntu",
                            Price = 0.00m
                        },
                        new
                        {
                            OsId = new Guid("7991a3c5-adf6-4f55-b5fc-17671ea6a545"),
                            Name = "Windows 10",
                            Price = 30.00m
                        },
                        new
                        {
                            OsId = new Guid("fa451cd0-2140-4b9f-8e82-0bc0299214ae"),
                            Name = "Windows Server",
                            Price = 50.00m
                        },
                        new
                        {
                            OsId = new Guid("e903b466-7d93-4f73-be85-f3f7098480f6"),
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
                            RamId = new Guid("7483ac57-c3d0-4705-b974-9cb97bd9c3ec"),
                            Name = "2 GB",
                            Price = 30.00m
                        },
                        new
                        {
                            RamId = new Guid("2ae9780b-ddf6-49cb-8162-2a540d17f5a3"),
                            Name = "4 GB",
                            Price = 60.00m
                        },
                        new
                        {
                            RamId = new Guid("8d865fee-f704-490a-81a1-bc82a81fe12b"),
                            Name = "8 GB",
                            Price = 120.00m
                        },
                        new
                        {
                            RamId = new Guid("cc0f8905-e488-463e-a609-dbe1d0a0a67e"),
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
                            RomId = new Guid("54d95712-3b02-4bf4-9af4-1ef627680b02"),
                            Name = "128 GB",
                            Price = 50.00m
                        },
                        new
                        {
                            RomId = new Guid("c4852627-d7ff-41c0-bb2e-0e0797e3fe1c"),
                            Name = "256 GB",
                            Price = 100.00m
                        },
                        new
                        {
                            RomId = new Guid("74e656d7-5b28-4d63-b335-0caf9833b2a6"),
                            Name = "512 GB",
                            Price = 200.00m
                        },
                        new
                        {
                            RomId = new Guid("573a9c1f-5400-4e6c-9573-4766db55fd2d"),
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

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_setups_user_id");

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
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_setups_users_user_id");

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
