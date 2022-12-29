using HealthClinicApi.Models;

namespace HealthClinicApi.Dtos.AdmissionRecordDtos
{
    public class GetAdmissionRecordDto
    {
        public DateTime AdmittedAt { get; set; }
        public string? PatientName { get; set; }
        public string? DoctorName { get; set; }
        public Nullable<bool> Urgent { get; set; }
    }
}

