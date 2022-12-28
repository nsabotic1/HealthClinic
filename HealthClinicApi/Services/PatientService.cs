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
    }
}
