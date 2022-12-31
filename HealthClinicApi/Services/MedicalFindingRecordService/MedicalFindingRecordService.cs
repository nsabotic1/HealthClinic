using AutoMapper;
using HealthClinicApi.Data;
using HealthClinicApi.Dtos.MedicalFindingRecordDto;
using HealthClinicApi.Dtos.PatientDtos;
using HealthClinicApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthClinicApi.Services.MedicalFindingRecordService
{
    public class MedicalFindingRecordService : IMedicalFindingRecordService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public MedicalFindingRecordService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<ServiceResponse<GetMedicalFindingRecordDto>> AddMedicalFindingRecord(AddMedicalFindingRecordDto newMedicalFindingRecord)
        {
            var serviceResponse = new ServiceResponse<GetMedicalFindingRecordDto>();
            try
            {
                
                var patient = await _context.Patients.SingleOrDefaultAsync(d => d.Id == newMedicalFindingRecord.PatientTd);
               
                if (patient == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "The patient with that id doesn't exist!";
                    return serviceResponse;
                }
                
                var record = _mapper.Map<MedicalFindingRecord>(newMedicalFindingRecord);
                record.CreatedAt = DateTime.UtcNow;
                record.Patient = patient;
                _context.MedicalFindingRecords.Add(record);
                await _context.SaveChangesAsync();
               
                serviceResponse.Data = _mapper.Map<GetMedicalFindingRecordDto>(record);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetMedicalFindingRecordDto>>> GetAllMedicalFindingRecords()
        {
            var serviceResponse = new ServiceResponse<List<GetMedicalFindingRecordDto>>();
            try
            {
                var records = await _context.MedicalFindingRecords.ToListAsync();
                List<GetMedicalFindingRecordDto> allRecords = new List<GetMedicalFindingRecordDto>();
                GetMedicalFindingRecordDto helperRecord = new GetMedicalFindingRecordDto();

                foreach (var record in records)
                {
                    helperRecord = new GetMedicalFindingRecordDto();
                    var patient = await _context.Patients.SingleOrDefaultAsync(d => d.Id == record.PatientTd);

                    helperRecord.Id = record.Id;
                    helperRecord.Description = record.Description;
                    helperRecord.CreatedAt = record.CreatedAt;
                    helperRecord.Patient = _mapper.Map<GetPatientDto>(patient);
                    allRecords.Add(helperRecord);
                }
                serviceResponse.Data = allRecords;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public Task<ServiceResponse<GetMedicalFindingRecordDto>> GetMedicalFindingRecordByPatient(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<GetMedicalFindingRecordDto>> UpdateMedicalFindingRecord(int id, UpdateMedicalFindingRecordDto newMedicalFindingRecord)
        {
            throw new NotImplementedException();
        }
    }
}
