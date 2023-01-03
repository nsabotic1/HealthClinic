using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HealthClinicApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdmissionRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdmittedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: true),
                    DoctorId = table.Column<int>(type: "int", nullable: true),
                    Urgent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdmissionRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdmissionRecords_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AdmissionRecords_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MedicalFindingRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    AdmissionRecordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalFindingRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalFindingRecords_AdmissionRecords_AdmissionRecordId",
                        column: x => x.AdmissionRecordId,
                        principalTable: "AdmissionRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalFindingRecords_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "Code", "Lastname", "Name", "Title" },
                values: new object[,]
                {
                    { 1, 1221, "Arslanagić", "Teufik", 1 },
                    { 2, 3313, "Dizdarević", "Amira", 1 },
                    { 3, 4924, "Srećkić", "Srećko", 2 },
                    { 4, 8976, "Puhalo", "Simonida", 3 },
                    { 5, 8888, "Fazlinović", "Izet", 1 }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Adress", "Birthdate", "Gender", "Lastname", "Name", "Number" },
                values: new object[,]
                {
                    { 1, "Zahira Panjete 32", new DateTime(1980, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Mosby", "Ted", "+38761395783" },
                    { 2, "Neverland 812", new DateTime(1989, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Stinson", "Barney", "0603372400" },
                    { 3, "New Jersey 22", new DateTime(1999, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Vision", "Wanda", "062575685" },
                    { 4, "Ferde Hauptman 32", new DateTime(1970, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Green", "Rachel", "+4930901820" },
                    { 5, "Iza sedam mora i gora 92", new DateTime(2002, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Buffay", "Phoebe", "+3812509182509" }
                });

            migrationBuilder.InsertData(
                table: "AdmissionRecords",
                columns: new[] { "Id", "AdmittedAt", "DoctorId", "PatientId", "Urgent" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, true },
                    { 2, new DateTime(2023, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, false },
                    { 3, new DateTime(2023, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 3, true },
                    { 4, new DateTime(2023, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 4, false },
                    { 5, new DateTime(2023, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 5, true }
                });

            migrationBuilder.InsertData(
                table: "MedicalFindingRecords",
                columns: new[] { "Id", "AdmissionRecordId", "CreatedAt", "Description", "PatientId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 1, 3, 13, 24, 54, 513, DateTimeKind.Utc).AddTicks(9125), "The patient complains of kidney pain. Sand present in right kidney. Do a urine test", 1 },
                    { 2, 2, new DateTime(2023, 1, 3, 13, 24, 54, 513, DateTimeKind.Utc).AddTicks(9129), "Asthma present for months. Need to change therapy.", 2 },
                    { 3, 3, new DateTime(2023, 1, 3, 13, 24, 54, 513, DateTimeKind.Utc).AddTicks(9132), "The patient complains of back pain. it is necessary to take a spine scan", 3 },
                    { 4, 4, new DateTime(2023, 1, 3, 13, 24, 54, 513, DateTimeKind.Utc).AddTicks(9134), "Sore throat for 10 days. Drink as much tea as possible and Tylol hot.", 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdmissionRecords_DoctorId",
                table: "AdmissionRecords",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_AdmissionRecords_PatientId",
                table: "AdmissionRecords",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalFindingRecords_AdmissionRecordId",
                table: "MedicalFindingRecords",
                column: "AdmissionRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalFindingRecords_PatientId",
                table: "MedicalFindingRecords",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalFindingRecords");

            migrationBuilder.DropTable(
                name: "AdmissionRecords");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
