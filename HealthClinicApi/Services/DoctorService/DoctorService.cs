using AutoMapper;
using HealthClinicApi.Data;
using HealthClinicApi.Dtos.DoctorDtos;
using HealthClinicApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthClinicApi.Services.DoctorService
{
    public class DoctorService : IDoctorService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public DoctorService( IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<GetDoctorDto>> AddDoctor(AddDoctorDto newDoctor)
        {
            var serviceResponse = new ServiceResponse<GetDoctorDto>();
            try
            {
                var doctor = _mapper.Map<Doctor>(newDoctor);
                _context.Doctors.Add(doctor);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetDoctorDto>(doctor);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetDoctorDto>>> GetAllDoctors()
        {
            var serviceResponse = new ServiceResponse<List<GetDoctorDto>>();
            try
            {
                var doctors = await _context.Doctors.ToListAsync();
                serviceResponse.Data = doctors.Select(p => _mapper.Map<GetDoctorDto>(p)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetDoctorDto>> GetDoctorById(int id)
        {
            var serviceResponse = new ServiceResponse<GetDoctorDto>();
            try
            {
                var doctor = await _context.Doctors.Where(p => p.Id == id).SingleOrDefaultAsync();
                serviceResponse.Data = _mapper.Map<GetDoctorDto>(doctor);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}
