namespace HealthClinicApi.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public Title Title { get; set; }
        public int Code { get; set; }
        public ICollection<AdmissionRecord>? AdmissionRecords { get; set; } //1 doktor ima vise prijema
    }
}
