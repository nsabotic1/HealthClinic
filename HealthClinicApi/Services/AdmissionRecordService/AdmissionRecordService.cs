using AutoMapper;
using HealthClinicApi.Data;
using HealthClinicApi.Dtos.AdmissionRecordDtos;
using HealthClinicApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthClinicApi.Services.AdmissionRecordService
{
    public class AdmissionRecordService : IAdmissionRecordService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public AdmissionRecordService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<GetAdmissionRecordDto>> AddAdmisionRecord(AddAdmissionRecordDto newRecord)
        {
            var serviceResponse = new ServiceResponse<GetAdmissionRecordDto>();
            try
            {
               if(DateTime.Compare(newRecord.AdmittedAt.Date,DateTime.Today)<0)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Admitted date can't be older than today!";
                    return serviceResponse;
                }
               var doctor = await _context.Doctors.SingleOrDefaultAsync(d => d.Id == newRecord.DoctorId);
               if(doctor!=null && doctor.Title != Title.Specialist)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "The doctor must be a specialist!";
                    return serviceResponse;
                }
               var patient = await _context.Patients.SingleOrDefaultAsync(p => p.Id == newRecord.PatientId);
               if(patient == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "The patient with that id doesn't exist!";
                    return serviceResponse;
                }
               if(doctor == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "The doctor with that id doesn't exist!";
                    return serviceResponse;
                }
                var record = _mapper.Map<AdmissionRecord>(newRecord);
                _context.AdmissionRecords.Add(record);
                await _context.SaveChangesAsync();
                string patientName = record.Patient.Name + " " + record.Patient.Lastname;
                string doctorName = record.Doctor.Name + " " + record.Doctor.Lastname + " - " + record.Doctor.Code;
                GetAdmissionRecordDto helperRecord = new GetAdmissionRecordDto();
                helperRecord.AdmittedAt = record.AdmittedAt;
                helperRecord.PatientName = patientName;
                helperRecord.DoctorName = doctorName;
                helperRecord.Urgent = record.Urgent;
                serviceResponse.Data = helperRecord;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetAdmissionRecordDto>>> GetAllAdmissionRecords()
        {
            var serviceResponse = new ServiceResponse<List<GetAdmissionRecordDto>>();
            try
            {
                var records = await _context.AdmissionRecords.ToListAsync();
                string patientName;
                string doctorName;
                List<GetAdmissionRecordDto> allRecords = new List<GetAdmissionRecordDto>();
                GetAdmissionRecordDto helperRecord = new GetAdmissionRecordDto();
                
                foreach (var record in records)
                {
                  helperRecord = new GetAdmissionRecordDto();
                  var doctor = await _context.Doctors.SingleOrDefaultAsync(d => d.Id == record.DoctorId);
                  
                  if(doctor != null)
                    {
                      doctorName = doctor.Name + " " + doctor.Lastname + " - " + record.Doctor.Code;
                        helperRecord.DoctorName = doctorName;
                    }
                  
                  var patient = await _context.Patients.SingleOrDefaultAsync(p => p.Id == record.PatientId);
                  if(patient != null)
                    {
                      patientName = patient.Name + " " + patient.Lastname;
                      helperRecord.PatientName = patientName;
                    }

                  helperRecord.AdmittedAt = record.AdmittedAt;
                  helperRecord.Urgent = record.Urgent;
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
