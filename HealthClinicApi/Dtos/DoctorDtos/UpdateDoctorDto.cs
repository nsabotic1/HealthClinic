using HealthClinicApi.Models;

namespace HealthClinicApi.Dtos.DoctorDtos
{
    public class UpdateDoctorDto
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public Title Title { get; set; }
        public int Code { get; set; }
    }
}
