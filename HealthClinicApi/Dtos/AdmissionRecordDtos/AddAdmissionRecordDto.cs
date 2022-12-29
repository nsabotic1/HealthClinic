using HealthClinicApi.Models;

namespace HealthClinicApi.Dtos.AdmissionRecordDtos
{
    public class AddAdmissionRecordDto
    {
        public DateTime AdmittedAt { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public Nullable<bool> Urgent { get; set; }
    }
}
