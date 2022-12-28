namespace HealthClinicApi.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthdate { get; set; }
        public Gender Gender { get; set; }
        public string? Adress { get; set; }
        public int? Number { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public ICollection<AdmissionRecord> AdmissionRecords { get; set; } //1 pacijent moze imati vise prijema
        public ICollection<MedicalFindingRecord> MedicalFindingRecords { get; set; } //1 pacijent moze imati vise prijema

    }
}
