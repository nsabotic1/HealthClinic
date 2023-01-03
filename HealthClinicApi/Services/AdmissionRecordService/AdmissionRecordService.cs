using AutoMapper;
using HealthClinicApi.Data;
using HealthClinicApi.Dtos.AdmissionRecordDtos;
using HealthClinicApi.Helpers;
using HealthClinicApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthClinicApi.Services.AdmissionRecordService
{
    public class AdmissionRecordService : IAdmissionRecordService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHelperMethodsService _helperMethods;

        public AdmissionRecordService(IMapper mapper, DataContext context, IHelperMethodsService helperMethods)
        {
            _helperMethods = helperMethods;
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
                
                GetAdmissionRecordDto helperRecord = new GetAdmissionRecordDto();
                helperRecord = await _helperMethods.ReturnAdmissionRecord(record);
               
                serviceResponse.Data = helperRecord;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetAdmissionRecordDto>>> DeleteAdmissionRecord(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetAdmissionRecordDto>>();
            try
            {
                var deletedRecord = await _context.AdmissionRecords.SingleOrDefaultAsync(r => r.Id == id);
                if(deletedRecord == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Admission record with that id doesn't exsist!";
                    return serviceResponse;
                }

                _context.AdmissionRecords.Remove(deletedRecord);
                await _context.SaveChangesAsync();

                var records = await _context.AdmissionRecords.ToListAsync();
               
                List<GetAdmissionRecordDto> allRecords = new List<GetAdmissionRecordDto>();
                GetAdmissionRecordDto helperRecord = new GetAdmissionRecordDto();

                foreach (var record in records)
                {
                    helperRecord = new GetAdmissionRecordDto();
                    helperRecord = await _helperMethods.ReturnAdmissionRecord(record);
                    allRecords.Add(helperRecord);
                }

                serviceResponse.Data = allRecords;
                serviceResponse.Message = "Record successfully deleted!";
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetAdmissionRecordDto>>> GetAllAdmissionRecords(DateTime? date1, DateTime? date2)
        {
            var serviceResponse = new ServiceResponse<List<GetAdmissionRecordDto>>();
            try
            {
                var records = await _context.AdmissionRecords.ToListAsync();
               
                List<GetAdmissionRecordDto> allRecords = new List<GetAdmissionRecordDto>();
                GetAdmissionRecordDto helperRecord = new GetAdmissionRecordDto();
                
                foreach (var record in records)
                {
                  helperRecord = new GetAdmissionRecordDto();
                  helperRecord = await _helperMethods.ReturnAdmissionRecord(record);
                  allRecords.Add(helperRecord);
                }

                if(date1!=null && date2 != null)
                {
                   allRecords = allRecords.Where(r => r.AdmittedAt.Date >= date1 && r.AdmittedAt.Date <= date2).ToList();
                }

                if((date1==null && date2!= null) || (date1!=null && date2 == null))
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "You must enter both dates!";
                    return serviceResponse;
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

        public async Task<ServiceResponse<GetAdmissionRecordDto>> UpdateAdmissionRecord(int id, UpdateAdmissionRecordDto newRecord)
        {
            var serviceResponse = new ServiceResponse<GetAdmissionRecordDto>();
            try
            {
                if (newRecord.AdmittedAt.HasValue)
                {
                    if (DateTime.Compare((DateTime)newRecord.AdmittedAt, DateTime.Today.Date) < 0)
                    {
                        serviceResponse.Success = false;
                        serviceResponse.Message = "Admitted date can't be older than today!";
                        return serviceResponse;
                    }
                }

                var updatedRecord = await _context.AdmissionRecords.SingleOrDefaultAsync(r => r.Id == id);

                if(updatedRecord == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Admission record with that id doesn't exsist!";
                    return serviceResponse;
                }

                var patient = await _context.Patients.SingleOrDefaultAsync(p => p.Id == newRecord.PatientId);
                var oldPatient = await _context.Patients.SingleOrDefaultAsync(p => p.Id == updatedRecord.PatientId);

                var doctor = await _context.Doctors.SingleOrDefaultAsync(d => d.Id == newRecord.DoctorId);
                var oldDoctor = await _context.Doctors.SingleOrDefaultAsync(d => d.Id == updatedRecord.DoctorId);

                if (newRecord.DoctorId != null) updatedRecord.DoctorId = newRecord.DoctorId;
                if (newRecord.PatientId != null) updatedRecord.PatientId = newRecord.PatientId;
                if (newRecord.Urgent != null) updatedRecord.Urgent = (bool)newRecord.Urgent;
                if (newRecord.AdmittedAt.HasValue) updatedRecord.AdmittedAt = (DateTime)newRecord.AdmittedAt;

                GetAdmissionRecordDto helperRecord = new GetAdmissionRecordDto();

                if(newRecord.AdmittedAt.HasValue) helperRecord.AdmittedAt = (DateTime)newRecord.AdmittedAt;
                else helperRecord.AdmittedAt = updatedRecord.AdmittedAt;

                if (patient != null)
                {
                    string patientName = patient.Name + " " + patient.Lastname;
                    helperRecord.PatientName = patientName;
                }
                else if(oldPatient != null)
                {
                    helperRecord.PatientName = oldPatient.Name + " " + oldPatient.Lastname;
                }

                if (doctor != null && doctor.Title == Title.Specialist)
                {
                    string doctorName = doctor.Name + " " + doctor.Lastname + " - " + doctor.Code;
                    helperRecord.DoctorName = doctorName;
                }
                else if(oldDoctor != null)
                {
                    helperRecord.DoctorName = oldDoctor.Name + " " + oldDoctor.Lastname + " - " + oldDoctor.Code;
                }
                else if(doctor!=null && doctor.Title != Title.Specialist)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "The doctor must be a specialist!";
                    return serviceResponse;
                }
                if (updatedRecord.Urgent == true) helperRecord.Urgent = "Yes";
                else helperRecord.Urgent = "No";
                helperRecord.Id = updatedRecord.Id;

                await _context.SaveChangesAsync();
                serviceResponse.Data = helperRecord;
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
