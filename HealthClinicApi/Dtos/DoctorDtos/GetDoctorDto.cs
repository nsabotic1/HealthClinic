using HealthClinicApi.Models;

namespace HealthClinicApi.Dtos.DoctorDtos
{
    public class GetDoctorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public Title Title { get; set; }
        public int Code { get; set; }
    }
}
