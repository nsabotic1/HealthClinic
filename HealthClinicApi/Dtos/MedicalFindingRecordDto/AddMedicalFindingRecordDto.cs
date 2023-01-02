namespace HealthClinicApi.Dtos.MedicalFindingRecordDto
{
    public class AddMedicalFindingRecordDto
    {
        public string Description { get; set; }
        public int PatientId { get; set; }
        public int AdmissionRecordId { get; set; }
    }
}
