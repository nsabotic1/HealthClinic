﻿using HealthClinicApi.Dtos.PatientDtos;
using HealthClinicApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthClinicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _patientService.GetAllPatients();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            var response = await _patientService.GetPatientById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddPatientDto newPatient)
        {
            var response = await _patientService.AddPatient(newPatient);
            if(response.Data == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
