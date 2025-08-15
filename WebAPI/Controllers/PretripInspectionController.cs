using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PretripInspectionController : ControllerBase
    {
        private readonly IPretripInspectionService _service;

        public PretripInspectionController(IPretripInspectionService service)
        {
            _service = service;
        }

        [HttpGet]
        [EnableQuery]
        public IActionResult Get()
        {
            var inspections = _service.GetAll();
            return Ok(inspections);
        }
    }
}
