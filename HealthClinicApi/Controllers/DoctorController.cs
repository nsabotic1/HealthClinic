using HealthClinicApi.Dtos.DoctorDtos;
using HealthClinicApi.Services.DoctorService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthClinicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _doctorService.GetAllDoctors();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            var response = await _doctorService.GetDoctorById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddDoctorDto newDoctor)
        {
            var response = await _doctorService.AddDoctor(newDoctor);
            if (response.Data == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
