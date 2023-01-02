using AutoMapper;
using HealthClinicApi.Data;
using HealthClinicApi.Dtos.AdmissionRecordDtos;
using HealthClinicApi.Dtos.MedicalFindingRecordDto;
using HealthClinicApi.Dtos.PatientDtos;
using HealthClinicApi.Helpers;
using HealthClinicApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthClinicApi.Services.MedicalFindingRecordService
{
    public class MedicalFindingRecordService : IMedicalFindingRecordService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHelperMethodsService _helperMethods;

        public MedicalFindingRecordService(IMapper mapper, DataContext context, IHelperMethodsService helperMethods)
        {
            _mapper = mapper;
            _context = context;
            _helperMethods = helperMethods;
        }
        public async Task<ServiceResponse<GetMedicalFindingRecordDto>> AddMedicalFindingRecord(AddMedicalFindingRecordDto newMedicalFindingRecord)
        {
            var serviceResponse = new ServiceResponse<GetMedicalFindingRecordDto>();
            try
            {
                
                var patient = await _context.Patients.SingleOrDefaultAsync(d => d.Id == newMedicalFindingRecord.PatientId);
                if (patient == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "The patient with that id doesn't exist!";
                    return serviceResponse;
                }

                var admissionRecord = await _context.AdmissionRecords.SingleOrDefaultAsync(r => r.Id == newMedicalFindingRecord.AdmissionRecordId);
                
                if (admissionRecord == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "The admission record with that id doesn't exist!";
                    return serviceResponse;
                }
                if (admissionRecord.PatientId != patient.Id)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "That admission record doesn't belog to that patient!";
                    return serviceResponse;
                }
                
                var record = _mapper.Map<MedicalFindingRecord>(newMedicalFindingRecord);
                record.CreatedAt = DateTime.UtcNow;
                record.Patient = patient;
                record.AdmissionRecord = admissionRecord;
                _context.MedicalFindingRecords.Add(record);
                await _context.SaveChangesAsync();
                var helperRecord = new GetMedicalFindingRecordDto();
                helperRecord = await _helperMethods.ReturnMedicalRecord(record);
                Console.WriteLine(record);
                serviceResponse.Data = helperRecord;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        
        public async Task<ServiceResponse<List<GetMedicalFindingRecordDto>>> DeleteMedicalFindingRecord(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetMedicalFindingRecordDto>>();
            try
            {
                var deletedRecord = await _context.MedicalFindingRecords.SingleOrDefaultAsync(r => r.Id == id);
                if (deletedRecord == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Medical record with that id doesn't exsist!";
                    return serviceResponse;
                }
                _context.MedicalFindingRecords.Remove(deletedRecord);
                await _context.SaveChangesAsync();

                var records = await _context.MedicalFindingRecords.ToListAsync();
                List<GetMedicalFindingRecordDto> allRecords = new List<GetMedicalFindingRecordDto>();
                GetMedicalFindingRecordDto helperRecord = new GetMedicalFindingRecordDto();
                
                foreach (var record in records)
                {
                    helperRecord = new GetMedicalFindingRecordDto();
                    helperRecord = await _helperMethods.ReturnMedicalRecord(record);
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
                    helperRecord = await _helperMethods.ReturnMedicalRecord(record);
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

        public async Task<ServiceResponse<List<GetMedicalFindingRecordDto>>> GetMedicalFindingRecordByPatient(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetMedicalFindingRecordDto>>();
            try
            {
                var records = await _context.MedicalFindingRecords
                    .Where(c => c.PatientId == id)
                    .ToListAsync();
                List<GetMedicalFindingRecordDto> allRecords = new List<GetMedicalFindingRecordDto>();
                GetMedicalFindingRecordDto helperRecord = new GetMedicalFindingRecordDto();
                if(records.Count == 0)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Patient with that id doesn't exist or doesn't have any medical records!";
                    return serviceResponse;
                }
                foreach (var record in records)
                {
                    helperRecord = new GetMedicalFindingRecordDto();
                    helperRecord = await _helperMethods.ReturnMedicalRecord(record);
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

        public async Task<ServiceResponse<GetMedicalFindingRecordDto>> UpdateMedicalFindingRecord(int id, UpdateMedicalFindingRecordDto newRecord)
        {
            var serviceResponse = new ServiceResponse<GetMedicalFindingRecordDto>();
            try
            {
                
                var updatedRecord = await _context.MedicalFindingRecords.SingleOrDefaultAsync(r => r.Id == id);

                if (updatedRecord == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Medical record with that id doesn't exsist!";
                    return serviceResponse;
                }
                if (string.IsNullOrWhiteSpace(newRecord.Description))
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Description can't be empty or only contain whitespaces!";
                    return serviceResponse;
                }

                _mapper.Map(newRecord, updatedRecord);
                await _context.SaveChangesAsync();

                GetMedicalFindingRecordDto helperMedicalRecord = new GetMedicalFindingRecordDto();
                helperMedicalRecord = await _helperMethods.ReturnMedicalRecord(updatedRecord);
       
                serviceResponse.Data = helperMedicalRecord;
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
