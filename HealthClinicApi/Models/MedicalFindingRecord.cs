namespace HealthClinicApi.Models
{
    public class MedicalFindingRecord //nalaz
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int PatientId { get; set; }
        public Patient Patient { get; set; } //1 nalaz pripada 1 pacijentu
        public int AdmissionRecordId { get; set; }
        public AdmissionRecord AdmissionRecord { get; set; }

    }
}
