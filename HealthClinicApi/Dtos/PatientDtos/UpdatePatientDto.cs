using HealthClinicApi.Models;

namespace HealthClinicApi.Dtos.PatientDtos
{
    public class UpdatePatientDto
    {
        public string? Name { get; set; }
        public string? Lastname { get; set; }
        public DateTime? Birthdate { get; set; }
        public Gender? Gender { get; set; }
        public string? Adress { get; set; }
        public string? Number { get; set; }
    }
}
