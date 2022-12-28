using HealthClinicApi.Models;

namespace HealthClinicApi.Dtos.DoctorDtos
{
    public class AddDoctorDto
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public Title Title { get; set; }
        public int Code { get; set; }
    }
}
