﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PedroTer7.FinancialMonkey.IdentityServer.Contexts;

#nullable disable

namespace PedroTer7.FinancialMonkey.IdentityServer.Migrations
{
    [DbContext(typeof(CredentialsContext))]
    [Migration("20240323192203_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("PedroTer7.FinancialMonkey.IdentityServer.Entities.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("UTC_TIMESTAMP(0)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(74)
                        .HasColumnType("varchar(74)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Admins");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreationDateTime = new DateTime(2024, 3, 23, 16, 22, 3, 559, DateTimeKind.Local).AddTicks(5323),
                            Email = "adm1@financialmonkey.com",
                            Password = "U1HI2N4NY:71f200f2e9db3190d8d675356c9d7a802b606d9841d715c7fbc7a4a9bf216e6e"
                        },
                        new
                        {
                            Id = 2,
                            CreationDateTime = new DateTime(2024, 3, 23, 16, 22, 3, 559, DateTimeKind.Local).AddTicks(5410),
                            Email = "adm2@financialmonkey.com",
                            Password = "9YR3WEX5F:69006a0d4fd00e5216ac69fe71588b7bc78a8f1144120b4bc46e3c448ccd2112"
                        });
                });

            modelBuilder.Entity("PedroTer7.FinancialMonkey.IdentityServer.Entities.ClientCredential", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AllowedGrantTypes")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("AllowedScopes")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<DateTime>("CreationDateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("UTC_TIMESTAMP(0)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)");

                    b.Property<string>("Secret")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("Key")
                        .IsUnique();

                    b.ToTable("ClientCredentials");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AllowedGrantTypes = "password",
                            AllowedScopes = "openid admin",
                            CreationDateTime = new DateTime(2024, 3, 23, 16, 22, 3, 559, DateTimeKind.Local).AddTicks(6466),
                            Description = "Client for admin users authentication",
                            Key = "6f65a068-e943-11ee-b79b-03fb35a7a73f",
                            Name = "Admin auth client",
                            Secret = "9297320e-e943-11ee-882f-bf8013a44c6f"
                        },
                        new
                        {
                            Id = 2,
                            AllowedGrantTypes = "password",
                            AllowedScopes = "openid customer",
                            CreationDateTime = new DateTime(2024, 3, 23, 16, 22, 3, 559, DateTimeKind.Local).AddTicks(6503),
                            Description = "Client for customer users authentication",
                            Key = "ca32537e-e943-11ee-b968-2f83f8d52888",
                            Name = "Customer auth client",
                            Secret = "d0dec608-e943-11ee-ad8c-532040233037"
                        });
                });

            modelBuilder.Entity("PedroTer7.FinancialMonkey.IdentityServer.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("UTC_TIMESTAMP(0)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(74)
                        .HasColumnType("varchar(74)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreationDateTime = new DateTime(2024, 3, 23, 16, 22, 3, 559, DateTimeKind.Local).AddTicks(3541),
                            Email = "customer_1@gmail.com",
                            Password = "PVYH7CA0N:224efedf74992dfc29264369b1c2ee93cb36065e2c3b6efc34410756ee894792"
                        },
                        new
                        {
                            Id = 2,
                            CreationDateTime = new DateTime(2024, 3, 23, 16, 22, 3, 559, DateTimeKind.Local).AddTicks(3839),
                            Email = "customer_2@somecorp.com",
                            Password = "7NYRKQYNR:f077f875d10879f19c6cd54a59ff0052f584b2dd9eb2b10d82407318e9c97f5a"
                        },
                        new
                        {
                            Id = 3,
                            CreationDateTime = new DateTime(2024, 3, 23, 16, 22, 3, 559, DateTimeKind.Local).AddTicks(3904),
                            Email = "customer_3@thirdcopr.com",
                            Password = "LEYUOPYZ3:d01f90250d2ae834059ac01c61e88da5d2b79c36a156bef3a6b67a374497aa8e"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
