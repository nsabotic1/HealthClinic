using HealthClinicApi.Models;

namespace HealthClinicApi.Dtos.PatientDtos
{
    public class AddPatientDto
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthdate { get; set; }
        public Gender Gender { get; set; }
        public string? Adress { get; set; }
        public int? Number { get; set; }
    }
}
