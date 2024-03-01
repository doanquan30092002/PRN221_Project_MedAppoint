﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PRN221_Project_MedAppoint.Model;

#nullable disable

namespace PRN221_Project_MedAppoint.Migrations
{
    [DbContext(typeof(MyMedDbContext))]
    partial class MyMedDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.27")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PRN221_Project_MedAppoint.Model.Appointments", b =>
                {
                    b.Property<int>("AppointmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AppointmentID"), 1L, 1);

                    b.Property<int?>("DoctorID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("Date");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int?>("SpecialtyID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("Date");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int?>("UserID")
                        .HasColumnType("int");

                    b.HasKey("AppointmentID");

                    b.HasIndex("DoctorID");

                    b.HasIndex("SpecialtyID");

                    b.HasIndex("UserID");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("PRN221_Project_MedAppoint.Model.DoctorDayOff", b =>
                {
                    b.Property<int>("DoctorDayOffID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DoctorDayOffID"), 1L, 1);

                    b.Property<int?>("DoctorID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("Date");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Reasons")
                        .HasColumnType("Ntext");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("Date");

                    b.HasKey("DoctorDayOffID");

                    b.HasIndex("DoctorID");

                    b.ToTable("DoctorDayOffs");
                });

            modelBuilder.Entity("PRN221_Project_MedAppoint.Model.DoctorReviews", b =>
                {
                    b.Property<int>("ReviewID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReviewID"), 1L, 1);

                    b.Property<string>("Comment")
                        .HasColumnType("NTEXT");

                    b.Property<int?>("DoctorID")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ReviewDate")
                        .HasColumnType("Date");

                    b.Property<int?>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ReviewID");

                    b.HasIndex("DoctorID");

                    b.HasIndex("UserID");

                    b.ToTable("DoctorReviews");
                });

            modelBuilder.Entity("PRN221_Project_MedAppoint.Model.ElectronicMedicalRecords", b =>
                {
                    b.Property<int>("EMR_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EMR_ID"), 1L, 1);

                    b.Property<int?>("AppointmentID")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("Date");

                    b.Property<string>("TestResults")
                        .HasColumnType("NTEXT");

                    b.Property<string>("TreatmentPlans")
                        .HasColumnType("NTEXT");

                    b.HasKey("EMR_ID");

                    b.HasIndex("AppointmentID");

                    b.ToTable("ElectronicMedicalRecords");
                });

            modelBuilder.Entity("PRN221_Project_MedAppoint.Model.HealthInformation", b =>
                {
                    b.Property<int>("HealthInfoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HealthInfoID"), 1L, 1);

                    b.Property<string>("Allergies")
                        .HasColumnType("NTEXT");

                    b.Property<string>("BloodType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HealthHistory")
                        .HasColumnType("NTEXT");

                    b.Property<float?>("Height")
                        .HasColumnType("real");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Medications")
                        .HasColumnType("NTEXT");

                    b.Property<int?>("UserID")
                        .HasColumnType("int");

                    b.Property<float?>("Weight")
                        .HasColumnType("real");

                    b.HasKey("HealthInfoID");

                    b.HasIndex("UserID");

                    b.ToTable("HealthInformations");
                });

            modelBuilder.Entity("PRN221_Project_MedAppoint.Model.Payments", b =>
                {
                    b.Property<int>("PaymentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentID"), 1L, 1);

                    b.Property<decimal?>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("AppointmentID")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .HasColumnType("NTEXT");

                    b.Property<DateTime?>("PaymentDate")
                        .HasColumnType("Date");

                    b.Property<int?>("UserID")
                        .HasColumnType("int");

                    b.HasKey("PaymentID");

                    b.HasIndex("AppointmentID");

                    b.HasIndex("UserID");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("PRN221_Project_MedAppoint.Model.Roles", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleID"), 1L, 1);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleID");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("PRN221_Project_MedAppoint.Model.Services", b =>
                {
                    b.Property<int>("ServiceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ServiceID"), 1L, 1);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<decimal?>("Price")
                        .IsRequired()
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ServiceID");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("PRN221_Project_MedAppoint.Model.Specialist", b =>
                {
                    b.Property<int>("SpecialtyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SpecialtyID"), 1L, 1);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("SpecialtyName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SpecialtyID");

                    b.ToTable("Specialists");
                });

            modelBuilder.Entity("PRN221_Project_MedAppoint.Model.SpecialistToService", b =>
                {
                    b.Property<int>("SpecialistToServiceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SpecialistToServiceID"), 1L, 1);

                    b.Property<int>("ServiceID")
                        .HasColumnType("int");

                    b.Property<int>("SpecialistID")
                        .HasColumnType("int");

                    b.HasKey("SpecialistToServiceID");

                    b.HasIndex("ServiceID");

                    b.HasIndex("SpecialistID");

                    b.ToTable("SpecialistToServices");
                });

            modelBuilder.Entity("PRN221_Project_MedAppoint.Model.Users", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"), 1L, 1);

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .HasColumnType("NVARCHAR(255)");

                    b.Property<string>("Avatar")
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR(255)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Gender")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR(255)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoleID")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasMaxLength(255)
                        .HasColumnType("NVARCHAR(255)");

                    b.HasKey("UserID");

                    b.HasIndex("RoleID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PRN221_Project_MedAppoint.Model.UsersToSpecialist", b =>
                {
                    b.Property<int>("UsersToSpecialistID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UsersToSpecialistID"), 1L, 1);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("SpecialistSpecialtyID")
                        .HasColumnType("int");

                    b.Property<int?>("SpecialtyID")
                        .HasColumnType("int");

                    b.Property<int?>("UserID")
                        .HasColumnType("int");

                    b.HasKey("UsersToSpecialistID");

                    b.HasIndex("SpecialistSpecialtyID");

                    b.HasIndex("UserID");

                    b.ToTable("UsersToSpecialists");
                });

            modelBuilder.Entity("PRN221_Project_MedAppoint.Model.Appointments", b =>
                {
                    b.HasOne("PRN221_Project_MedAppoint.Model.Users", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorID");

                    b.HasOne("PRN221_Project_MedAppoint.Model.Specialist", "Specialty")
                        .WithMany()
                        .HasForeignKey("SpecialtyID");

                    b.HasOne("PRN221_Project_MedAppoint.Model.Users", "User")
                        .WithMany()
                        .HasForeignKey("UserID");

                    b.Navigation("Doctor");

                    b.Navigation("Specialty");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PRN221_Project_MedAppoint.Model.DoctorDayOff", b =>
                {
                    b.HasOne("PRN221_Project_MedAppoint.Model.Users", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorID");

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("PRN221_Project_MedAppoint.Model.DoctorReviews", b =>
                {
                    b.HasOne("PRN221_Project_MedAppoint.Model.Users", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorID");

                    b.HasOne("PRN221_Project_MedAppoint.Model.Users", "User")
                        .WithMany()
                        .HasForeignKey("UserID");

                    b.Navigation("Doctor");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PRN221_Project_MedAppoint.Model.ElectronicMedicalRecords", b =>
                {
                    b.HasOne("PRN221_Project_MedAppoint.Model.Appointments", "Appointment")
                        .WithMany()
                        .HasForeignKey("AppointmentID");

                    b.Navigation("Appointment");
                });

            modelBuilder.Entity("PRN221_Project_MedAppoint.Model.HealthInformation", b =>
                {
                    b.HasOne("PRN221_Project_MedAppoint.Model.Users", "User")
                        .WithMany()
                        .HasForeignKey("UserID");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PRN221_Project_MedAppoint.Model.Payments", b =>
                {
                    b.HasOne("PRN221_Project_MedAppoint.Model.Appointments", "Appointment")
                        .WithMany()
                        .HasForeignKey("AppointmentID");

                    b.HasOne("PRN221_Project_MedAppoint.Model.Users", "User")
                        .WithMany()
                        .HasForeignKey("UserID");

                    b.Navigation("Appointment");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PRN221_Project_MedAppoint.Model.SpecialistToService", b =>
                {
                    b.HasOne("PRN221_Project_MedAppoint.Model.Services", "Services")
                        .WithMany()
                        .HasForeignKey("ServiceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PRN221_Project_MedAppoint.Model.Specialist", "Specialist")
                        .WithMany()
                        .HasForeignKey("SpecialistID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Services");

                    b.Navigation("Specialist");
                });

            modelBuilder.Entity("PRN221_Project_MedAppoint.Model.Users", b =>
                {
                    b.HasOne("PRN221_Project_MedAppoint.Model.Roles", "Role")
                        .WithMany()
                        .HasForeignKey("RoleID");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("PRN221_Project_MedAppoint.Model.UsersToSpecialist", b =>
                {
                    b.HasOne("PRN221_Project_MedAppoint.Model.Specialist", "Specialist")
                        .WithMany()
                        .HasForeignKey("SpecialistSpecialtyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PRN221_Project_MedAppoint.Model.Users", "User")
                        .WithMany()
                        .HasForeignKey("UserID");

                    b.Navigation("Specialist");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
