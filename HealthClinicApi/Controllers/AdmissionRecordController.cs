using HealthClinicApi.Dtos.AdmissionRecordDtos;
using HealthClinicApi.Services.AdmissionRecordService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthClinicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdmissionRecordController : ControllerBase
    {
        private readonly IAdmissionRecordService _admissionRecordService;

        public AdmissionRecordController(IAdmissionRecordService admissionRecordService)
        {
            _admissionRecordService = admissionRecordService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _admissionRecordService.GetAllAdmissionRecords();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddAdmissionRecordDto newRecord)
        {
            var response = await _admissionRecordService.AddAdmisionRecord(newRecord);
            if (response.Data == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
