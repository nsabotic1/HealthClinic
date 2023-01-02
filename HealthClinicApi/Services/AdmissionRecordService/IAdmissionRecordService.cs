using HealthClinicApi.Dtos.AdmissionRecordDtos;
using HealthClinicApi.Models;

namespace HealthClinicApi.Services.AdmissionRecordService
{
    public interface IAdmissionRecordService
    {
        Task<ServiceResponse<List<GetAdmissionRecordDto>>> GetAllAdmissionRecords(DateTime? date1, DateTime? date2);
        Task<ServiceResponse<GetAdmissionRecordDto>> AddAdmisionRecord(AddAdmissionRecordDto newRecord);
        Task<ServiceResponse<GetAdmissionRecordDto>> UpdateAdmissionRecord(int id, UpdateAdmissionRecordDto newRecord);
        Task<ServiceResponse<List<GetAdmissionRecordDto>>> DeleteAdmissionRecord(int id);
    }
}
