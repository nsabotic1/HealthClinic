using HealthClinicApi.Data;
using HealthClinicApi.Dtos.AdmissionRecordDtos;
using HealthClinicApi.Dtos.MedicalFindingRecordDto;
using HealthClinicApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthClinicApi.Helpers
{
    public class HelperMethodsService : IHelperMethodsService
    {
        private readonly DataContext _context;

        public HelperMethodsService(DataContext context)
        {
            _context = context;
        }

        public async Task<GetAdmissionRecordDto> ReturnAdmissionRecord(AdmissionRecord record)
        {
            var doctor = await _context.Doctors.SingleOrDefaultAsync(d => d.Id == record.DoctorId);
            string patientName;
            string doctorName;
            GetAdmissionRecordDto helperRecord = new GetAdmissionRecordDto();
            if (doctor != null)
            {
                doctorName = doctor.Name + " " + doctor.Lastname + " - " + record.Doctor.Code;
                helperRecord.DoctorName = doctorName;
            }

            var patient = await _context.Patients.SingleOrDefaultAsync(p => p.Id == record.PatientId);
            if (patient != null)
            {
                patientName = patient.Name + " " + patient.Lastname;
                helperRecord.PatientName = patientName;
            }

            helperRecord.Id = record.Id;
            helperRecord.AdmittedAt = record.AdmittedAt;
            if (record.Urgent == true) helperRecord.Urgent = "Yes";
            else helperRecord.Urgent = "No";

            return helperRecord;
        }

        public async Task<GetMedicalFindingRecordDto> ReturnMedicalRecord(MedicalFindingRecord record)
        {
            
            GetMedicalFindingRecordDto helperRecord = new GetMedicalFindingRecordDto();
            GetAdmissionRecordDto helperAdmissionRecord = new GetAdmissionRecordDto();
            string patientName;
            string doctorName;
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
            return helperRecord;
        }
    }
}
