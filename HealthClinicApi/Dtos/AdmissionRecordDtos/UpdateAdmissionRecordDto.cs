namespace HealthClinicApi.Dtos.AdmissionRecordDtos
{
    public class UpdateAdmissionRecordDto
    {
        public DateTime? AdmittedAt { get; set; }
        public int? PatientId { get; set; }
        public int? DoctorId { get; set; }
        public bool? Urgent { get; set; }
    }
}
