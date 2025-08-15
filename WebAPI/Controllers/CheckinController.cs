using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CheckinController : ControllerBase
    {
        private readonly ICheckinService _checkinService;

        public CheckinController(ICheckinService checkinService)
        {
            _checkinService = checkinService;
        }

        // GET /api/checkin?$top=10&$filter=UsedInDcu eq true
        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> GetAll()
        {
            var result = await _checkinService.GetAllAsync();
            return Ok(result);
        }

        // POST api/checkin/modify
        [HttpPost("modify")]
        public async Task<IActionResult> ModifyAsync([FromBody] ModifyCheckinsRequest request)
        {
            if (request?.Datas == null || !request.Datas.Any())
                return BadRequest("Data is required.");

            var insertItems = request.Datas
                .Where(d => !string.IsNullOrWhiteSpace(d.CheckoutDate) && !string.IsNullOrWhiteSpace(d.AmtEmployeeId))
                .ToList();

            var updateItems = request.Datas
                .Where(d => string.IsNullOrWhiteSpace(d.CheckoutDate) && !string.IsNullOrWhiteSpace(d.AmtEmployeeId))
                .ToList();

            if (insertItems.Any())
            {
                var insertResult = await _checkinService.ModifyInsertAsync(insertItems);
                if (!insertResult.Success)
                    return BadRequest(insertResult.Message);
            }

            if (updateItems.Any())
            {
                var updateResult = await _checkinService.ModifyUpdateAsync(updateItems);
                if (!updateResult.Success)
                    return BadRequest(updateResult.Message);
            }

            return Ok("Operation successful.");
        }
    }
}
