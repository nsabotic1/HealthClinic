using HealthClinicApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthClinicApi.Data
{
    public class SeedData
    {
        public static void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Doctor>().HasData(new Doctor
            {
                Id = 1,
                Name = "Teufik",
                Lastname = "Arslanagić",
                Title = Title.Specialist,
                Code = 1221
            },
            new Doctor{
                Id = 2,
                Name = "Amira",
                Lastname = "Dizdarević",
                Title = Title.Specialist,
                Code = 3313
            },
            new Doctor
            {
                Id = 3,
                Name = "Srećko",
                Lastname = "Srećkić",
                Title = Title.Resident,
                Code = 4924
            },
            new Doctor
            {
                Id = 4,
                Name = "Simonida",
                Lastname = "Puhalo",
                Title = Title.Nurse,
                Code = 8976
            },
            new Doctor
            {
                Id = 5,
                Name = "Izet",
                Lastname = "Fazlinović",
                Title = Title.Specialist,
                Code = 8888
            });
            modelBuilder.Entity<Patient>().HasData(new Patient
            {
                Id = 1,
                Name = "Ted",
                Lastname = "Mosby",
                Birthdate = DateTime.ParseExact("15/06/1980", "dd/MM/yyyy", null),
                Gender = Gender.Male,
                Adress = "Zahira Panjete 32",
                Number = "+38761395783"
            },
            new Patient
            {
                Id = 2,
                Name = "Barney",
                Lastname = "Stinson",
                Birthdate = DateTime.ParseExact("12/12/1989", "dd/MM/yyyy", null),
                Gender = Gender.Male,
                Adress = "Neverland 812",
                Number = "0603372400"
            },
             new Patient
             {
                 Id = 3,
                 Name = "Wanda",
                 Lastname = "Vision",
                 Birthdate = DateTime.ParseExact("01/07/1999", "dd/MM/yyyy", null),
                 Gender = Gender.Female,
                 Adress = "New Jersey 22",
                 Number = "062575685"
             },
              new Patient
              {
                  Id = 4,
                  Name = "Rachel",
                  Lastname = "Green",
                  Birthdate = DateTime.ParseExact("21/05/1970", "dd/MM/yyyy", null),
                  Gender = Gender.Female,
                  Adress = "Ferde Hauptman 32",
                  Number = "+4930901820"
              },
              new Patient
              {
                  Id = 5,
                  Name = "Phoebe",
                  Lastname = "Buffay",
                  Birthdate = DateTime.ParseExact("10/10/2002", "dd/MM/yyyy", null),
                  Gender = Gender.Female,
                  Adress = "Iza sedam mora i gora 92",
                  Number = "+3812509182509"
              });

            modelBuilder.Entity<AdmissionRecord>().HasData(
                new AdmissionRecord
                {
                    Id = 1,
                    AdmittedAt = DateTime.ParseExact("21/05/2023", "dd/MM/yyyy", null),
                    PatientId = 1,
                    DoctorId = 1,
                    Urgent = true
                },
                new AdmissionRecord
                {
                    Id = 2,
                    AdmittedAt = DateTime.ParseExact("18/05/2023", "dd/MM/yyyy", null),
                    PatientId = 2,
                    DoctorId = 2,
                    Urgent = false
                },
                new AdmissionRecord
                {
                    Id = 3,
                    AdmittedAt = DateTime.ParseExact("30/06/2023", "dd/MM/yyyy", null),
                    PatientId = 3,
                    DoctorId = 5,
                    Urgent = true
                },
                new AdmissionRecord
                {
                    Id = 4,
                    AdmittedAt = DateTime.ParseExact("05/01/2023", "dd/MM/yyyy", null),
                    PatientId = 4,
                    DoctorId = 1,
                    Urgent = false
                },
                new AdmissionRecord
                {
                    Id = 5,
                    AdmittedAt = DateTime.ParseExact("02/02/2023", "dd/MM/yyyy", null),
                    PatientId = 5,
                    DoctorId = 2,
                    Urgent = true
                });

            modelBuilder.Entity<MedicalFindingRecord>().HasData(
                new MedicalFindingRecord
                {
                    Id = 1,
                    Description = "The patient complains of kidney pain. Sand present in right kidney. Do a urine test",
                    CreatedAt = DateTime.UtcNow,
                    PatientId = 1,
                    AdmissionRecordId = 1
                },
                new MedicalFindingRecord
                {
                    Id = 2,
                    Description = "Asthma present for months. Need to change therapy.",
                    CreatedAt = DateTime.UtcNow,
                    PatientId = 2,
                    AdmissionRecordId = 2
                },
                new MedicalFindingRecord
                {
                    Id = 3,
                    Description = "The patient complains of back pain. it is necessary to take a spine scan",
                    CreatedAt = DateTime.UtcNow,
                    PatientId = 3,
                    AdmissionRecordId = 3
                 },
                new MedicalFindingRecord
                {
                    Id = 4,
                    Description = "Sore throat for 10 days. Drink as much tea as possible and Tylol hot.",
                    CreatedAt = DateTime.UtcNow,
                    PatientId = 4,
                    AdmissionRecordId = 4
                });
        

    }
    }
}
