using HealthClinicApi.Models;

namespace HealthClinicApi.Dtos.AdmissionRecordDtos
{
    public class GetAdmissionRecordDto
    {
        public int Id { get; set; }
        public DateTime AdmittedAt { get; set; }
        public string? PatientName { get; set; }
        public string? DoctorName { get; set; }
        public string Urgent { get; set; }
    }
}

