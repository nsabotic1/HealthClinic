using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HealthClinicApi.Migrations
{
    /// <inheritdoc />
    public partial class PatientsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Adress", "Birthdate", "DoctorId", "Gender", "Lastname", "Name", "Number" },
                values: new object[,]
                {
                    { 1, "Zahira Panjete 32", new DateTime(1980, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Mosby", "Ted", 123 },
                    { 2, "Neverland 812", new DateTime(1989, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Stinson", "Barney", 321 },
                    { 3, "New Jersey 22", new DateTime(1999, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, "Vision", "Wanda", 541 },
                    { 4, "Ferde Hauptman 32", new DateTime(1970, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, "Green", "Rachel", 541 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
