using AutoMapper;
using HealthClinicApi.Dtos.AdmissionRecordDtos;
using HealthClinicApi.Dtos.DoctorDtos;
using HealthClinicApi.Dtos.MedicalFindingRecordDto;
using HealthClinicApi.Dtos.PatientDtos;
using HealthClinicApi.Models;

namespace HealthClinicApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Patient, GetPatientDto>();
            CreateMap<AddPatientDto, Patient>();
            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<UpdatePatientDto, Patient>()
           .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Gender?, Gender>().ConvertUsing((src, dest) => src ?? dest);

            CreateMap<Doctor, GetDoctorDto>();
            CreateMap<AddDoctorDto, Doctor>();
            CreateMap<UpdateDoctorDto, Doctor>()
           .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Title?, Title>().ConvertUsing((src, dest) => src ?? dest);

            CreateMap<AdmissionRecord, GetAdmissionRecordDto>();
            CreateMap<AddAdmissionRecordDto, AdmissionRecord>();
            

            CreateMap<MedicalFindingRecord, GetMedicalFindingRecordDto>();
            CreateMap<AddMedicalFindingRecordDto, MedicalFindingRecord>().ForMember(x => x.CreatedAt, t => t.Ignore()); ;
            CreateMap<UpdateMedicalFindingRecordDto, MedicalFindingRecord>();
        }
    }
}
