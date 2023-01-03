using AutoMapper;
using HealthClinicApi.Data;
using HealthClinicApi.Dtos.PatientDtos;
using HealthClinicApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

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
                Regex validatePhoneNumberRegex = new Regex("^\\+?[0-9][0-9]{7,14}$");

                if (newPatient.Number != null) {
                    if (!validatePhoneNumberRegex.IsMatch(newPatient.Number))
                    {
                        serviceResponse.Success = false;
                        serviceResponse.Message = "Invalid phone number!";
                        return serviceResponse;
                    }
                }
                if(string.IsNullOrWhiteSpace(newPatient.Name) || string.IsNullOrWhiteSpace(newPatient.Lastname))
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Name and Lastname can't be empty or contain only whitespaces!";
                    return serviceResponse;
                }
                var patient = _mapper.Map<Patient>(newPatient);
                if(patient.Gender == 0)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "You must choose a gender!";
                    return serviceResponse;
                }

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
                if (patient == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Patient with that id doesn't exist!";
                    return serviceResponse;
                }
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
                if (patient == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Patient with that id doesn't exist!";
                    return serviceResponse;
                }

                Regex validatePhoneNumberRegex = new Regex("^\\+?[0-9][0-9]{7,14}$");
                if (newPatient.Number != null)
                {
                    if (!validatePhoneNumberRegex.IsMatch(newPatient.Number))
                    {
                        serviceResponse.Success = false;
                        serviceResponse.Message = "Invalid phone number!";
                        return serviceResponse;
                    }
                }
               
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
                if (patient == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Patient with that id doesn't exist!";
                    return serviceResponse;
                }

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
