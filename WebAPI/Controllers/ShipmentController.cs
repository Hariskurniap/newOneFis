using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Common.Responses;
using Domain.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ShipmentController : ControllerBase
    {
        private readonly IShipmentService _shipmentService;
        private readonly ISapShipmentService _sapShipmentService;

        public ShipmentController(IShipmentService shipmentService, ISapShipmentService sapShipmentService)
        {
            _shipmentService = shipmentService;
            _sapShipmentService = sapShipmentService;
        }

        #region GET SHIPMENTS
        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            var shipments = await _shipmentService.GetShipmentsAsync();
            return Ok(shipments);
        }

        [HttpGet("GetShipmentStatuses")]
        public IActionResult GetShipmentStatuses()
        {
            var statuses = ShipmentStatus.GetAllWithColor();
            return Ok(ApiResponse<Dictionary<string, string>>.Success(statuses));
        }

        #endregion

        #region POST SHIPMENT
        [HttpPost("modify")]
        public async Task<IActionResult> Modify([FromBody] ModifyShipmentRequest request)
        {
            try
            {
                var result = await _shipmentService.UpdateShipmentAsync(request);
                if (result)
                    return Ok(new { message = "Success" });
                return BadRequest(new { message = "Update gagal" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        #endregion

        #region SAP
        [HttpPost("Sap/Shipment")]
        public async Task<IActionResult> PostToSap([FromBody] SapShipmentRequest request)
        {
            try
            {
                var result = await _sapShipmentService.PostShipmentSapAsync(
                    request.OnefisShipmentCode,
                    request.UpdatedAt,
                    request.UpdatedBy
                );
                return Ok(new { success = true, message = "Shipment sent to SAP successfully", data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("Sap/ShipmentGi")]
        public async Task<IActionResult> PostToSapGi([FromBody] SapShipmentRequest request)
        {
            try
            {
                var result = await _sapShipmentService.PostShipmentGiSapAsync(
                    request.OnefisShipmentCode,
                    request.UpdatedAt,
                    request.UpdatedBy
                );
                return Ok(new { success = true, message = "GI Shipment sent to SAP successfully", data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
        #endregion
    }
}
