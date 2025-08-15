using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DcuCheckinController : ControllerBase
{
    private readonly IDcuCheckinService _service;

    public DcuCheckinController(IDcuCheckinService service)
    {
        _service = service;
    }

    [HttpGet]
    [EnableQuery]
    public async Task<IActionResult> Get()
    {
        var data = await _service.GetAllAsync();
        return Ok(data);
    }

    [HttpPost("modify")]
    public async Task<IActionResult> Modify([FromBody] DcuCheckin data)
    {
        if (data == null)
            return BadRequest("Data is required.");

        if (string.IsNullOrWhiteSpace(data.AmtEmployeeId))
            return BadRequest("amtEmployeeId is required.");

        bool success = false;

        if (data.Status == "unused")
        {
            // Update dokumen yang ada berdasarkan Id
            if (string.IsNullOrWhiteSpace(data.Id))
                return BadRequest("Id is required for updating unused data.");

            success = await _service.ModifyAsync(data.Id, data); // Update logic di service
            if (!success)
                return NotFound("Record not found or nothing updated.");
        }
        else if (data.Status == "used")
        {
            // Insert dokumen baru
            data.CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            data.UpdatedAt = data.CreatedAt;

            success = await _service.InsertAsync(data); // Insert logic di service
        }
        else
        {
            return BadRequest("Invalid status value. Must be 'used' or 'unused'.");
        }

        return Ok("Operation successful.");
    }

}
