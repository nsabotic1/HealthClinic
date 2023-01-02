using HealthClinicApi.Dtos.AdmissionRecordDtos;
using HealthClinicApi.Dtos.MedicalFindingRecordDto;
using HealthClinicApi.Models;

namespace HealthClinicApi.Helpers
{
    public interface IHelperMethodsService
    {
        Task<GetMedicalFindingRecordDto> ReturnMedicalRecord(MedicalFindingRecord record);
        Task<GetAdmissionRecordDto> ReturnAdmissionRecord(AdmissionRecord record);
    }
}
