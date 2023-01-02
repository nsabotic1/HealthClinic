using HealthClinicApi.Dtos.AdmissionRecordDtos;
using HealthClinicApi.Dtos.PatientDtos;
using HealthClinicApi.Models;

namespace HealthClinicApi.Dtos.MedicalFindingRecordDto
{
    public class GetMedicalFindingRecordDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public GetAdmissionRecordDto AdmissionRecord { get; set; }
    }
}
