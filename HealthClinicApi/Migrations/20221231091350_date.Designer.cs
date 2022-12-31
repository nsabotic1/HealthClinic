﻿// <auto-generated />
using System;
using HealthClinicApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HealthClinicApi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20221231091350_date")]
    partial class date
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HealthClinicApi.Models.AdmissionRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AdmittedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DoctorId")
                        .HasColumnType("int");

                    b.Property<int?>("PatientId")
                        .HasColumnType("int");

                    b.Property<bool?>("Urgent")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("AdmissionRecords");
                });

            modelBuilder.Entity("HealthClinicApi.Models.Doctor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Title")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Doctors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = 1221,
                            Lastname = "Arslanagić",
                            Name = "Teufik",
                            Title = 1
                        },
                        new
                        {
                            Id = 2,
                            Code = 3313,
                            Lastname = "Dizdarević",
                            Name = "Amira",
                            Title = 1
                        },
                        new
                        {
                            Id = 3,
                            Code = 4924,
                            Lastname = "Srećkić",
                            Name = "Srećko",
                            Title = 2
                        },
                        new
                        {
                            Id = 4,
                            Code = 8976,
                            Lastname = "Puhalo",
                            Name = "Simonida",
                            Title = 3
                        });
                });

            modelBuilder.Entity("HealthClinicApi.Models.MedicalFindingRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<int>("PatientTd")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("MedicalFindingRecords");
                });

            modelBuilder.Entity("HealthClinicApi.Models.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Adress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DoctorId")
                        .HasColumnType("int");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Number")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.ToTable("Patients");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Adress = "Zahira Panjete 32",
                            Birthdate = new DateTime(1980, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Gender = 1,
                            Lastname = "Mosby",
                            Name = "Ted",
                            Number = "+38761395783"
                        },
                        new
                        {
                            Id = 2,
                            Adress = "Neverland 812",
                            Birthdate = new DateTime(1989, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Gender = 1,
                            Lastname = "Stinson",
                            Name = "Barney",
                            Number = "0603372400"
                        },
                        new
                        {
                            Id = 3,
                            Adress = "New Jersey 22",
                            Birthdate = new DateTime(1999, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Gender = 2,
                            Lastname = "Vision",
                            Name = "Wanda",
                            Number = "062575685"
                        },
                        new
                        {
                            Id = 4,
                            Adress = "Ferde Hauptman 32",
                            Birthdate = new DateTime(1970, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Gender = 2,
                            Lastname = "Green",
                            Name = "Rachel",
                            Number = "+4930901820"
                        });
                });

            modelBuilder.Entity("HealthClinicApi.Models.AdmissionRecord", b =>
                {
                    b.HasOne("HealthClinicApi.Models.Doctor", "Doctor")
                        .WithMany("AdmissionRecords")
                        .HasForeignKey("DoctorId");

                    b.HasOne("HealthClinicApi.Models.Patient", "Patient")
                        .WithMany("AdmissionRecords")
                        .HasForeignKey("PatientId");

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("HealthClinicApi.Models.MedicalFindingRecord", b =>
                {
                    b.HasOne("HealthClinicApi.Models.Patient", "Patient")
                        .WithMany("MedicalFindingRecords")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("HealthClinicApi.Models.Patient", b =>
                {
                    b.HasOne("HealthClinicApi.Models.Doctor", null)
                        .WithMany("Patients")
                        .HasForeignKey("DoctorId");
                });

            modelBuilder.Entity("HealthClinicApi.Models.Doctor", b =>
                {
                    b.Navigation("AdmissionRecords");

                    b.Navigation("Patients");
                });

            modelBuilder.Entity("HealthClinicApi.Models.Patient", b =>
                {
                    b.Navigation("AdmissionRecords");

                    b.Navigation("MedicalFindingRecords");
                });
#pragma warning restore 612, 618
        }
    }
}
