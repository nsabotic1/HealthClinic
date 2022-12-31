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
              });
        }
    }
}
