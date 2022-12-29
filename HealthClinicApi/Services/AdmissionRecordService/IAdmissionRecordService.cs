using HealthClinicApi.Dtos.AdmissionRecordDtos;
using HealthClinicApi.Models;

namespace HealthClinicApi.Services.AdmissionRecordService
{
    public interface IAdmissionRecordService
    {
        Task<ServiceResponse<List<GetAdmissionRecordDto>>> GetAllAdmissionRecords();
        Task<ServiceResponse<GetAdmissionRecordDto>> AddAdmisionRecord(AddAdmissionRecordDto newRecord);
    }
}
