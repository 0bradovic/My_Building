﻿// <auto-generated />
using System;
using MOJA_ZGRADA.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MOJA_ZGRADA.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("MOJA_ZGRADA.Context.MyRoleManager", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("MOJA_ZGRADA.Data.Account", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("First_Name");

                    b.Property<string>("Last_Name");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("MOJA_ZGRADA.Data.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("Date_Of_Birth");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("First_Name")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<string>("JMBG")
                        .IsRequired()
                        .HasMaxLength(13);

                    b.Property<string>("Last_Name")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<string>("PhoneNumber")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("Email", "JMBG")
                        .IsUnique();

                    b.ToTable("tbl_Admin");
                });

            modelBuilder.Entity("MOJA_ZGRADA.Data.Building", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("Date_Of_Creation");

                    b.Property<string>("Nickname")
                        .HasMaxLength(20);

                    b.Property<int>("Number_Of_Apartments")
                        .HasMaxLength(3);

                    b.Property<int>("Number_Of_Basements")
                        .HasMaxLength(3);

                    b.Property<int>("Number_Of_Entrances")
                        .HasMaxLength(3);

                    b.Property<int>("Number_Of_Floors")
                        .HasMaxLength(3);

                    b.Property<int>("Number_Of_Parking_Places")
                        .HasMaxLength(3);

                    b.Property<int>("Number_Of_Tenants")
                        .HasMaxLength(3);

                    b.Property<bool>("Special_Apartments_Annotation");

                    b.HasKey("Id");

                    b.HasIndex("Address")
                        .IsUnique();

                    b.ToTable("tbl_Building");
                });

            modelBuilder.Entity("MOJA_ZGRADA.Data.Cleaning_Plan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Cleaning_Price")
                        .HasMaxLength(50);

                    b.Property<string>("Cleaning_Type")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("Cleaning_Type", "Cleaning_Price")
                        .IsUnique();

                    b.ToTable("tbl_Cleaning_Plan");
                });

            modelBuilder.Entity("MOJA_ZGRADA.Data.Created_Cleaning_Plan", b =>
                {
                    b.Property<int>("Cleaning_Plan_Id");

                    b.Property<int>("Building_Id");

                    b.Property<int>("Admin_Id");

                    b.Property<DateTime>("Cleaning_DateTime");

                    b.Property<DateTime>("Cleaning_Issued_DateTime");

                    b.HasKey("Cleaning_Plan_Id", "Building_Id", "Admin_Id");

                    b.ToTable("tbl_Created_Cleaning_Plan");
                });

            modelBuilder.Entity("MOJA_ZGRADA.Data.Handles", b =>
                {
                    b.Property<int>("Admin_Id");

                    b.Property<int>("Building_Id");

                    b.Property<DateTime>("Started_Working_DateTime");

                    b.HasKey("Admin_Id", "Building_Id");

                    b.ToTable("tbl_Handles");
                });

            modelBuilder.Entity("MOJA_ZGRADA.Data.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Invoice_Amount")
                        .HasMaxLength(50);

                    b.Property<string>("Invoice_Type")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("Invoice_Type", "Invoice_Amount")
                        .IsUnique();

                    b.ToTable("tbl_Invoice");
                });

            modelBuilder.Entity("MOJA_ZGRADA.Data.Issued_Invoice", b =>
                {
                    b.Property<int>("Invoice_Id");

                    b.Property<int>("Tenant_Id");

                    b.Property<int>("Admin_Id");

                    b.Property<int>("Building_Id");

                    b.Property<DateTime>("Invoice_Creation_DateTime");

                    b.HasKey("Invoice_Id", "Tenant_Id", "Admin_Id", "Building_Id");

                    b.ToTable("tbl_Issued_Invoice");
                });

            modelBuilder.Entity("MOJA_ZGRADA.Data.Lives_In", b =>
                {
                    b.Property<int>("Tenant_Id");

                    b.Property<int>("Building_Id");

                    b.Property<DateTime>("Started_Living_DateTime");

                    b.HasKey("Tenant_Id", "Building_Id");

                    b.ToTable("tbl_Lives_In");
                });

            modelBuilder.Entity("MOJA_ZGRADA.Data.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Admin_Id");

                    b.Property<string>("File_URL");

                    b.Property<int>("Tenant_Id");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("tbl_Message");
                });

            modelBuilder.Entity("MOJA_ZGRADA.Data.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("tbl_Notification");
                });

            modelBuilder.Entity("MOJA_ZGRADA.Data.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Admin_Id");

                    b.Property<int>("Building_Id");

                    b.Property<string>("File_URL");

                    b.Property<DateTime>("Post_Creation_DateTime");

                    b.Property<DateTime>("Post_LifeTime_DateTime");

                    b.Property<string>("Post_Name")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<string>("Post_Priority")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<DateTime>("Post_Update_DateTime");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("tbl_Post");
                });

            modelBuilder.Entity("MOJA_ZGRADA.Data.Tenant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Apartment_Number")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<DateTime>("Date_Of_Birth");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("First_Name")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<int>("Floor_Number")
                        .HasMaxLength(3);

                    b.Property<string>("JMBG")
                        .IsRequired()
                        .HasMaxLength(13);

                    b.Property<string>("Last_Name")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<int>("Number_Of_Occupants")
                        .HasMaxLength(3);

                    b.Property<string>("PhoneNumber")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("Email", "JMBG", "Address")
                        .IsUnique();

                    b.ToTable("tbl_Tenant");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("MOJA_ZGRADA.Context.MyRoleManager")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("MOJA_ZGRADA.Data.Account")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("MOJA_ZGRADA.Data.Account")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("MOJA_ZGRADA.Context.MyRoleManager")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MOJA_ZGRADA.Data.Account")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("MOJA_ZGRADA.Data.Account")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}