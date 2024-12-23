﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UPBus_API;

#nullable disable

namespace UPBus_API.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20241206161824_addDailyGateAcc")]
    partial class addDailyGateAcc
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("UPBus_API.Entities.Bus", b =>
                {
                    b.Property<string>("BusNo")
                        .HasColumnType("varchar(7)");

                    b.Property<bool?>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("CreatedUser")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("DriverName")
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("NoOfSeat")
                        .HasColumnType("int");

                    b.Property<string>("Owner")
                        .HasColumnType("varchar(5)");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("UpdatedUser")
                        .HasColumnType("varchar(50)");

                    b.HasKey("BusNo");

                    b.ToTable("Bus");
                });

            modelBuilder.Entity("UPBus_API.Entities.DailyGateAcc", b =>
                {
                    b.Property<string>("GateCode")
                        .HasColumnType("varchar(4)");

                    b.Property<DateTime?>("AccDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("CreatedUser")
                        .HasColumnType("varchar(50)");

                    b.Property<double?>("ExpTotalAmt")
                        .HasColumnType("float");

                    b.Property<double?>("IncCreditAmt")
                        .HasColumnType("float");

                    b.Property<double?>("IncReceiveAmt")
                        .HasColumnType("float");

                    b.Property<double?>("IncTotalAmt")
                        .HasColumnType("float");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("UpdatedUser")
                        .HasColumnType("varchar(50)");

                    b.HasKey("GateCode", "AccDate");

                    b.ToTable("DailyGateAcc");
                });

            modelBuilder.Entity("UPBus_API.Entities.DailyGateExpense", b =>
                {
                    b.Property<string>("ExpNo")
                        .HasColumnType("varchar(15)");

                    b.Property<double?>("Amount")
                        .HasColumnType("float");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("CreatedUser")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("ExpCode")
                        .HasColumnType("varchar(6)");

                    b.Property<DateTime?>("ExpDate")
                        .HasColumnType("datetime");

                    b.Property<string>("GateCode")
                        .HasColumnType("varchar(4)");

                    b.Property<string>("PaidType")
                        .HasColumnType("varchar(6)");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("UpdatedUser")
                        .HasColumnType("varchar(50)");

                    b.HasKey("ExpNo");

                    b.ToTable("DailyGateExpense");
                });

            modelBuilder.Entity("UPBus_API.Entities.DailyGateIncome", b =>
                {
                    b.Property<string>("IncNo")
                        .HasColumnType("varchar(15)");

                    b.Property<double?>("Amount")
                        .HasColumnType("float");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("CreatedUser")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("GateCode")
                        .HasColumnType("varchar(4)");

                    b.Property<string>("IncCode")
                        .HasColumnType("varchar(6)");

                    b.Property<DateTime?>("IncDate")
                        .HasColumnType("datetime");

                    b.Property<string>("PaidType")
                        .HasColumnType("varchar(6)");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("UpdatedUser")
                        .HasColumnType("varchar(50)");

                    b.HasKey("IncNo");

                    b.ToTable("DailyGateIncome");
                });

            modelBuilder.Entity("UPBus_API.Entities.DailyPlan", b =>
                {
                    b.Property<string>("RegNo")
                        .HasColumnType("varchar(8)");

                    b.Property<string>("BusNo")
                        .HasColumnType("varchar(7)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("CreatedUser")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("DriverName")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Track")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TrackCode")
                        .HasColumnType("varchar(15)");

                    b.Property<string>("TrackType")
                        .HasColumnType("varchar(4)");

                    b.Property<DateTime?>("TripDate")
                        .HasColumnType("datetime");

                    b.Property<string>("TripTime")
                        .HasColumnType("nvarchar(5)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("UpdatedUser")
                        .HasColumnType("varchar(50)");

                    b.HasKey("RegNo");

                    b.ToTable("DailyPlan");
                });

            modelBuilder.Entity("UPBus_API.Entities.ExpenseType", b =>
                {
                    b.Property<string>("ExpCode")
                        .HasColumnType("varchar(6)");

                    b.Property<bool?>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("CreatedUser")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ExpName")
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("ExpType")
                        .HasColumnType("varchar(15)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("UpdatedUser")
                        .HasColumnType("varchar(50)");

                    b.HasKey("ExpCode");

                    b.ToTable("ExpenseType");
                });

            modelBuilder.Entity("UPBus_API.Entities.GasStation", b =>
                {
                    b.Property<string>("GSCode")
                        .HasColumnType("varchar(5)");

                    b.Property<bool?>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("CreatedUser")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("GSName")
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(500)");

                    b.Property<double?>("TotalBalance")
                        .HasColumnType("float");

                    b.Property<string>("Unit")
                        .HasColumnType("varchar(5)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("UpdatedUser")
                        .HasColumnType("varchar(50)");

                    b.HasKey("GSCode");

                    b.ToTable("GasStation");
                });

            modelBuilder.Entity("UPBus_API.Entities.Gate", b =>
                {
                    b.Property<string>("GateCode")
                        .HasColumnType("varchar(4)");

                    b.Property<bool?>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("CreatedUser")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("GateName")
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("UpdatedUser")
                        .HasColumnType("varchar(50)");

                    b.HasKey("GateCode");

                    b.ToTable("Gate");
                });

            modelBuilder.Entity("UPBus_API.Entities.IncomeType", b =>
                {
                    b.Property<string>("IncCode")
                        .HasColumnType("varchar(6)");

                    b.Property<bool?>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("CreatedUser")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("IncName")
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("IncType")
                        .HasColumnType("varchar(15)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("UpdatedUser")
                        .HasColumnType("varchar(50)");

                    b.HasKey("IncCode");

                    b.ToTable("IncomeType");
                });

            modelBuilder.Entity("UPBus_API.Entities.TrackType", b =>
                {
                    b.Property<string>("TripCode")
                        .HasColumnType("varchar(15)");

                    b.Property<bool?>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("CreatedUser")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("TripType")
                        .HasColumnType("varchar(4)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("UpdatedUser")
                        .HasColumnType("varchar(50)");

                    b.HasKey("TripCode");

                    b.ToTable("TrackType");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
