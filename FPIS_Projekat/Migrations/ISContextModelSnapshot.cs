﻿// <auto-generated />
using System;
using FPIS_Projekat.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FPIS_Projekat.Migrations
{
    [DbContext(typeof(ISContext))]
    partial class ISContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FPIS_Projekat.Models.Client", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telephone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("FPIS_Projekat.Models.Device", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ManufacturerID")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("ID");

                    b.HasIndex("ManufacturerID");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("FPIS_Projekat.Models.Employee", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("FPIS_Projekat.Models.Manufacturer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Manufacturers");
                });

            modelBuilder.Entity("FPIS_Projekat.Models.Offer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClientID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ClientID");

                    b.HasIndex("EmployeeID");

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("FPIS_Projekat.Models.OfferItem", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OfferID")
                        .HasColumnType("int");

                    b.Property<int>("TariffPackageID")
                        .HasColumnType("int");

                    b.Property<int?>("_DeviceID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("OfferID");

                    b.HasIndex("TariffPackageID");

                    b.HasIndex("_DeviceID");

                    b.ToTable("OfferItems");
                });

            modelBuilder.Entity("FPIS_Projekat.Models.PackageType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("PackageTypes");
                });

            modelBuilder.Entity("FPIS_Projekat.Models.TariffPackage", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfMB")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfMessages")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfMinutes")
                        .HasColumnType("int");

                    b.Property<int?>("PackageTypeID")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("ID");

                    b.HasIndex("PackageTypeID");

                    b.ToTable("TariffPackages");
                });

            modelBuilder.Entity("FPIS_Projekat.Models.Device", b =>
                {
                    b.HasOne("FPIS_Projekat.Models.Manufacturer", "_Manufacturer")
                        .WithMany()
                        .HasForeignKey("ManufacturerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("_Manufacturer");
                });

            modelBuilder.Entity("FPIS_Projekat.Models.Offer", b =>
                {
                    b.HasOne("FPIS_Projekat.Models.Client", "_Client")
                        .WithMany()
                        .HasForeignKey("ClientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FPIS_Projekat.Models.Employee", "_Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("_Client");

                    b.Navigation("_Employee");
                });

            modelBuilder.Entity("FPIS_Projekat.Models.OfferItem", b =>
                {
                    b.HasOne("FPIS_Projekat.Models.Offer", "_Offer")
                        .WithMany("OfferItems")
                        .HasForeignKey("OfferID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FPIS_Projekat.Models.TariffPackage", "_TariffPackage")
                        .WithMany()
                        .HasForeignKey("TariffPackageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FPIS_Projekat.Models.Device", "_Device")
                        .WithMany()
                        .HasForeignKey("_DeviceID");

                    b.Navigation("_Device");

                    b.Navigation("_Offer");

                    b.Navigation("_TariffPackage");
                });

            modelBuilder.Entity("FPIS_Projekat.Models.TariffPackage", b =>
                {
                    b.HasOne("FPIS_Projekat.Models.PackageType", "_PackageType")
                        .WithMany()
                        .HasForeignKey("PackageTypeID");

                    b.Navigation("_PackageType");
                });

            modelBuilder.Entity("FPIS_Projekat.Models.Offer", b =>
                {
                    b.Navigation("OfferItems");
                });
#pragma warning restore 612, 618
        }
    }
}
