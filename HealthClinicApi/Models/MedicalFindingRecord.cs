namespace HealthClinicApi.Models
{
    public class MedicalFindingRecord //nalaz
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int PatientTd { get; set; }
        public Patient Patient { get; set; } //1 nalaz pripada 1 pacijentu

    }
}
