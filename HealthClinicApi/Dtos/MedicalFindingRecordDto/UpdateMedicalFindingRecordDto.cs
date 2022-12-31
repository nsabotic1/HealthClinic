namespace HealthClinicApi.Dtos.MedicalFindingRecordDto
{
    public class UpdateMedicalFindingRecordDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int PatientTd { get; set; }
    }
}
