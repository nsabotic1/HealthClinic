using AutoMapper;
using HealthClinicApi.Data;
using HealthClinicApi.Dtos.AdmissionRecordDtos;
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
                string patientName;
                string doctorName;
                GetAdmissionRecordDto helperAdmissionRecord = new GetAdmissionRecordDto();
                helperAdmissionRecord = _mapper.Map<GetAdmissionRecordDto>(admissionRecord);
 
                var doctor = await _context.Doctors.SingleOrDefaultAsync(d => d.Id == admissionRecord.DoctorId);
                if (doctor != null)
                {
                    doctorName = doctor.Name + " " + doctor.Lastname + " - " + doctor.Code;
                    helperAdmissionRecord.DoctorName = doctorName;
                }
                if (patient != null)
                {
                    patientName = patient.Name + " " + patient.Lastname;
                    helperAdmissionRecord.PatientName = patientName;
                }
                var record = _mapper.Map<MedicalFindingRecord>(newMedicalFindingRecord);
                record.CreatedAt = DateTime.UtcNow;
                record.Patient = patient;
                record.AdmissionRecord = admissionRecord;
                _context.MedicalFindingRecords.Add(record);
                await _context.SaveChangesAsync();
                var helperRecord = new GetMedicalFindingRecordDto();
                helperRecord = _mapper.Map<GetMedicalFindingRecordDto>(record);
                helperRecord.AdmissionRecord = helperAdmissionRecord;
                serviceResponse.Data = helperRecord;
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
                GetAdmissionRecordDto helperAdmissionRecord = new GetAdmissionRecordDto();
                string patientName;
                string doctorName;
                foreach (var record in records)
                {
                    helperRecord = new GetMedicalFindingRecordDto();
                    helperAdmissionRecord = new GetAdmissionRecordDto();
                    var patient = await _context.Patients.SingleOrDefaultAsync(d => d.Id == record.PatientId);
                    var admissionRecord = await _context.AdmissionRecords.SingleOrDefaultAsync(d => d.Id == record.AdmissionRecordId);
                    var doctor = await _context.Doctors.SingleOrDefaultAsync(d => d.Id == admissionRecord.DoctorId);
                    if (doctor != null)
                    {
                        doctorName = doctor.Name + " " + doctor.Lastname + " - " + doctor.Code;
                        helperAdmissionRecord.DoctorName = doctorName;
                    }
                    if (patient != null)
                    {
                        patientName = patient.Name + " " + patient.Lastname;
                        helperAdmissionRecord.PatientName = patientName;
                    }
                    if (admissionRecord.Urgent == true) helperAdmissionRecord.Urgent = "Yes";
                    else helperAdmissionRecord.Urgent = "No";
                    helperAdmissionRecord.Id = admissionRecord.Id;
                    helperAdmissionRecord.AdmittedAt = admissionRecord.AdmittedAt;
                    helperRecord.Id = record.Id;
                    helperRecord.Description = record.Description;
                    helperRecord.CreatedAt = record.CreatedAt;
                    helperRecord.AdmissionRecord = helperAdmissionRecord;
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
                GetAdmissionRecordDto helperAdmissionRecord = new GetAdmissionRecordDto();
                string patientName;
                string doctorName;
                foreach (var record in records)
                {
                    helperRecord = new GetMedicalFindingRecordDto();
                    helperAdmissionRecord = new GetAdmissionRecordDto();
                    var patient = await _context.Patients.SingleOrDefaultAsync(d => d.Id == record.PatientId);
                    var admissionRecord = await _context.AdmissionRecords.SingleOrDefaultAsync(d => d.Id == record.AdmissionRecordId);
                    var doctor = await _context.Doctors.SingleOrDefaultAsync(d => d.Id == admissionRecord.DoctorId);
                    if (doctor != null)
                    {
                        doctorName = doctor.Name + " " + doctor.Lastname + " - " + doctor.Code;
                        helperAdmissionRecord.DoctorName = doctorName;
                    }
                    if (patient != null)
                    {
                        patientName = patient.Name + " " + patient.Lastname;
                        helperAdmissionRecord.PatientName = patientName;
                    }
                    if (admissionRecord.Urgent == true) helperAdmissionRecord.Urgent = "Yes";
                    else helperAdmissionRecord.Urgent = "No";
                    helperAdmissionRecord.Id = admissionRecord.Id;
                    helperAdmissionRecord.AdmittedAt = admissionRecord.AdmittedAt;
                    helperRecord.Id = record.Id;
                    helperRecord.Description = record.Description;
                    helperRecord.CreatedAt = record.CreatedAt;
                    helperRecord.AdmissionRecord = helperAdmissionRecord;
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

    }
}
