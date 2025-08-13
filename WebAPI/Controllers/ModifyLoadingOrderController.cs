using Application.Interfaces;
using Common.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;

namespace WebAPI.Controllers;

//[ApiController]
[Route("LoadingOrder/api/[controller]")]
[Authorize]
//[ApiExplorerSettings(GroupName = "LoadingOrder")]
public class ModifyLoadingOrderController : ControllerBase
{
    private readonly IListDOService _service;
    private readonly IMongoRepository _mongoRepository;

    public ModifyLoadingOrderController(
        IListDOService service,
        IMongoRepository mongoRepository)
    {
        _service = service;
        _mongoRepository = mongoRepository;
    }

    [HttpPost]
    public async Task<IActionResult> ModifyAsync([FromBody] ModifyLoadingOrderRequest request)
    {
        // Validasi input (tetap dilakukan di controller)
        if (string.IsNullOrWhiteSpace(request.DeliveryNumber) ||
            string.IsNullOrWhiteSpace(request.Plant) ||
            string.IsNullOrWhiteSpace(request.OnefisUpdatedBy) ||
            string.IsNullOrWhiteSpace(request.OnefisUpdatedAt))
        {
            return BadRequest(ApiResponse<object>.BadRequest("DeliveryNumber, Plant, OnefisUpdatedAt, and OnefisUpdatedBy are required."));
        }

        if (request.Datas == null)
        {
            return BadRequest(ApiResponse<object>.BadRequest("No data provided to update."));
        }

        // Inject audit fields
        request.Datas.OnefisUpdatedAt = request.OnefisUpdatedAt;
        request.Datas.OnefisUpdatedBy = request.OnefisUpdatedBy;

        var success = await _mongoRepository.ModifyListDOAsync(
            request.DeliveryNumber!,
            request.Plant!,
            request.Datas
        );

        if (!success)
        {
            return NotFound(ApiResponse<object>.NotFound("List DO not found or update failed."));
        }

        return Ok(ApiResponse<object>.Success("List DO updated successfully."));
    }
}
