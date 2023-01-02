using HealthClinicApi.Dtos.MedicalFindingRecordDto;
using HealthClinicApi.Services.MedicalFindingRecordService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthClinicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalFindingRecordController : ControllerBase
    {
        private readonly IMedicalFindingRecordService _medicalFindingRecordService;

        public MedicalFindingRecordController(IMedicalFindingRecordService medicalFindingRecordService)
        {
            _medicalFindingRecordService = medicalFindingRecordService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _medicalFindingRecordService.GetAllMedicalFindingRecords();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddMedicalFindingRecordDto newRecord)
        {
            var response = await _medicalFindingRecordService.AddMedicalFindingRecord(newRecord);
            if (response.Data == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            var response = await _medicalFindingRecordService.GetMedicalFindingRecordByPatient(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

    }
}
