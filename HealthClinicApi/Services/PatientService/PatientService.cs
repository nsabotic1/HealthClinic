using AutoMapper;
using HealthClinicApi.Data;
using HealthClinicApi.Dtos.PatientDtos;
using HealthClinicApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthClinicApi.Services
{
    public class PatientService : IPatientService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PatientService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetPatientDto>> AddPatient(AddPatientDto newPatient)
        {
            var serviceResponse = new ServiceResponse<GetPatientDto>();
            try
            {
                var patient = _mapper.Map<Patient>(newPatient);
                _context.Patients.Add(patient);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetPatientDto>(patient);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetPatientDto>>> GetAllPatients()
        {
            var serviceResponse = new ServiceResponse<List<GetPatientDto>>();
            try
            {
                var patients = await _context.Patients.ToListAsync();
                serviceResponse.Data = patients.Select(p=>_mapper.Map<GetPatientDto>(p)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            
            return serviceResponse;

        }

        public async Task<ServiceResponse<GetPatientDto>> GetPatientById(int id)
        {
            var serviceResponse = new ServiceResponse<GetPatientDto>();
            try
            {
                var patient = await _context.Patients.Where(p => p.Id == id).SingleOrDefaultAsync();
                serviceResponse.Data = _mapper.Map<GetPatientDto>(patient);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetPatientDto>> UpdatePatient(int id, UpdatePatientDto newPatient)
        {
            var serviceResponse = new ServiceResponse<GetPatientDto>();
            try
            {
                var patient = await _context.Patients.Where(p => p.Id == id).SingleOrDefaultAsync();
                _mapper.Map(newPatient, patient);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetPatientDto>(patient);
                serviceResponse.Message = "Your patient has been updated !";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GetPatientDto>>> DeletePatient(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetPatientDto>>();
            try
            {
                var patient = await _context.Patients.Where(p => p.Id == id).SingleOrDefaultAsync();
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _context.Patients.Select(p=>_mapper.Map<GetPatientDto>(p)).ToList();
                serviceResponse.Message = "Your patient has been deleted!";
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
