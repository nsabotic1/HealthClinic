﻿using HealthClinicApi.Dtos.PatientDtos;
using HealthClinicApi.Models;

namespace HealthClinicApi.Services
{
    public interface IPatientService
    {
        Task<ServiceResponse<List<GetPatientDto>>> GetAllPatients();
    }
}
