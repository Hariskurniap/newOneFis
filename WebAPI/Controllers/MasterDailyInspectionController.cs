using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using System.Linq;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MasterDailyInspectionController : ControllerBase
    {
        private readonly IMasterDailyInspectionService _service;

        public MasterDailyInspectionController(IMasterDailyInspectionService service)
        {
            _service = service;
        }

        [HttpGet]
        [EnableQuery]
        public IQueryable<MasterDailyInspection> GetOData()
        {
            return _service.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<MasterDailyInspection> GetById(string id)
        {
            var item = _service.GetById(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost("Modify")]
        public IActionResult Create([FromBody] MasterDailyInspection entity)
        {
            _service.Create(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }

        [HttpPost("Üpdate/{id}")]
        public IActionResult Update(string id, [FromBody] MasterDailyInspection entity)
        {
            var existing = _service.GetById(id);
            if (existing == null) return NotFound();

            _service.Update(id, entity);
            return NoContent();
        }
    }
}
