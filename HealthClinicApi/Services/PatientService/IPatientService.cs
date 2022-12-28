using HealthClinicApi.Dtos.PatientDtos;
using HealthClinicApi.Models;

namespace HealthClinicApi.Services
{
    public interface IPatientService
    {
        Task<ServiceResponse<List<GetPatientDto>>> GetAllPatients();
        Task<ServiceResponse<GetPatientDto>> GetPatientById(int id);
        Task<ServiceResponse<GetPatientDto>> AddPatient(AddPatientDto newPatient);
        Task<ServiceResponse<GetPatientDto>> UpdatePatient(int id, UpdatePatientDto newPatient);
        Task<ServiceResponse<List<GetPatientDto>>> DeletePatient(int id);
    }
}
