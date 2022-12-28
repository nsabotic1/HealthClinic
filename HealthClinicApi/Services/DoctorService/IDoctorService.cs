using HealthClinicApi.Dtos.DoctorDtos;
using HealthClinicApi.Models;

namespace HealthClinicApi.Services.DoctorService
{
    public interface IDoctorService
    {
        Task<ServiceResponse<List<GetDoctorDto>>> GetAllDoctors();
        Task<ServiceResponse<GetDoctorDto>> GetDoctorById(int id);
        Task<ServiceResponse<GetDoctorDto>> AddDoctor(AddDoctorDto newDoctor);
        Task<ServiceResponse<GetDoctorDto>> UpdateDoctor(int id, UpdateDoctorDto newDoctor);
        Task<ServiceResponse<List<GetDoctorDto>>> DeleteDoctor(int id);
    }
}
