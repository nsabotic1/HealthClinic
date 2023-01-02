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
        public async Task<IActionResult> GetAll([FromQuery] DateTime? startDate,[FromQuery] DateTime? endDate)
        {
            var response = await _admissionRecordService.GetAllAdmissionRecords(startDate, endDate);
            if (response.Data == null)
            {
                return BadRequest(response);
            }
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

        [HttpPut]
        public async Task<IActionResult> Update([FromQuery] int id, UpdateAdmissionRecordDto newRecord)
        {
            var response = await _admissionRecordService.UpdateAdmissionRecord(id, newRecord);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>Delete(int id)
        {
            var response = await _admissionRecordService.DeleteAdmissionRecord(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        } 
    }
}
