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

                if(doctor.Title == 0 || doctor.Code == 0)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Title and code can't be 0!";
                    return serviceResponse;
                }
                
                if(string.IsNullOrWhiteSpace(doctor.Name) || string.IsNullOrWhiteSpace(doctor.Lastname))
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Name and Lastname can't be empty";
                    return serviceResponse;
                }
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

        public async Task<ServiceResponse<List<GetDoctorDto>>> DeleteDoctor(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetDoctorDto>>();
            try
            {
                var doctor = await _context.Doctors.Where(d => d.Id == id).SingleOrDefaultAsync();
                if (doctor == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Doctor with that id doesn't exist!";
                    return serviceResponse;
                }

                _context.Doctors.Remove(doctor);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _context.Doctors.Select(d => _mapper.Map<GetDoctorDto>(d)).ToList();
                serviceResponse.Message = "Your doctor has been deleted!";
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
                if (doctor == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Doctor with that id doesn't exist!";
                    return serviceResponse;
                }
                serviceResponse.Data = _mapper.Map<GetDoctorDto>(doctor);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetDoctorDto>> UpdateDoctor(int id, UpdateDoctorDto newDoctor)
        {
            var serviceResponse = new ServiceResponse<GetDoctorDto>();
            try
            {
                var doctor = await _context.Doctors.Where(p => p.Id == id).SingleOrDefaultAsync();
                if(doctor == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Doctor with that id doesn't exist!";
                    return serviceResponse;
                }

                _mapper.Map(newDoctor, doctor);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetDoctorDto>(doctor);
                serviceResponse.Message = "Your doctor has been updated !";
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
