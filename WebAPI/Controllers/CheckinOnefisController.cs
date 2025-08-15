using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CheckinOnefisController : ControllerBase
    {
        private readonly ICheckinOnefisService _checkinOnefisService;

        public CheckinOnefisController(ICheckinOnefisService checkinOnefisService)
        {
            _checkinOnefisService = checkinOnefisService;
        }

        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> GetAll()
        {
            var result = await _checkinOnefisService.GetAllAsync();
            return Ok(result);
        }

        [HttpPost("modify")]
        public async Task<IActionResult> Modify([FromBody] CheckinOnefis checkinOnefis)
        {
            if (checkinOnefis == null || string.IsNullOrEmpty(checkinOnefis.AttendaceCode))
                return BadRequest("AttendaceCode wajib diisi.");

            // Set updated fields
            checkinOnefis.UpdatedAt = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
            checkinOnefis.UpdatedBy ??= "system";

            // Cek apakah data sudah ada
            var existing = await _checkinOnefisService.GetByAttendanceCodeAsync(checkinOnefis.AttendaceCode);
            if (existing == null)
            {
                // Set created fields
                checkinOnefis.CreatedAt = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
                checkinOnefis.CreatedBy ??= "system";
            }
            else
            {
                // Jika data sudah ada, bisa pertahankan CreatedAt/CreatedBy lama
                checkinOnefis.CreatedAt = existing.CreatedAt;
                checkinOnefis.CreatedBy = existing.CreatedBy;
            }

            await _checkinOnefisService.UpsertAsync(checkinOnefis);
            return Ok(checkinOnefis);
        }

    }
}
