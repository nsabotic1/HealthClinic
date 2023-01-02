using HealthClinicApi.Dtos.AdmissionRecordDtos;

namespace HealthClinicApi.Models
{
    public class AdmissionRecord //prijem pacijenata
    {
        public int Id { get; set; }
        public DateTime AdmittedAt { get; set; }
        public int? PatientId { get; set; } //1 prijem ima 1 pacijenta
        public int? DoctorId { get; set; } //1 prijem ima 1 doktora
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public bool Urgent{ get; set; }
        public ICollection<MedicalFindingRecord> MedicalFindingRecords { get; set; }
    }
}
