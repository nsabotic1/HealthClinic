namespace HealthClinicApi.Dtos.MedicalFindingRecordDto
{
    public class UpdateMedicalFindingRecordDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int PatientId { get; set; }
        public int AdmissionRecordId { get; set; }
    }
}
