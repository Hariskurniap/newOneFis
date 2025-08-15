using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DailyInspectionController : ControllerBase
    {
        private readonly IDailyInspectionService _service;

        public DailyInspectionController(IDailyInspectionService service)
        {
            _service = service;
        }

        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(data);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] DailyInspection inspection)
        {
            var success = await _service.CreateAsync(inspection);
            if (!success) return BadRequest("Failed to create record.");
            return Ok("Created successfully.");
        }

        [HttpPost("Update/{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] DailyInspection inspection)
        {
            var success = await _service.UpdateAsync(id, inspection);
            if (!success) return NotFound("Record not found or update failed.");
            return Ok("Updated successfully.");
        }
    }
}
