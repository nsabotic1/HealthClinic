using HealthClinicApi.Dtos.MedicalFindingRecordDto;
using HealthClinicApi.Models;

namespace HealthClinicApi.Services.MedicalFindingRecordService
{
    public interface IMedicalFindingRecordService
    {
        Task<ServiceResponse<List<GetMedicalFindingRecordDto>>> GetAllMedicalFindingRecords();
        Task<ServiceResponse<GetMedicalFindingRecordDto>> GetMedicalFindingRecordByPatient(int id);
        Task<ServiceResponse<GetMedicalFindingRecordDto>> AddMedicalFindingRecord(AddMedicalFindingRecordDto newMedicalFindingRecord);
        Task<ServiceResponse<GetMedicalFindingRecordDto>> UpdateMedicalFindingRecord(int id, UpdateMedicalFindingRecordDto newMedicalFindingRecord);
    }
}
