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
    public class ShipmentChecklistActivitiesController : ControllerBase
    {
        private readonly IShipmentChecklistActivitiesService _service;

        public ShipmentChecklistActivitiesController(IShipmentChecklistActivitiesService service)
        {
            _service = service;
        }

        // GET: api/ShipmentChecklistActivities
        [HttpGet]
        [EnableQuery]
        public IActionResult Get()
        {
            var data = _service.GetAll();
            return Ok(data);
        }

        // GET: api/ShipmentChecklistActivities/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var entity = _service.GetById(id);
            if (entity == null)
                return NotFound();

            return Ok(entity);
        }

        // POST: api/ShipmentChecklistActivities
        [HttpPost("Create")]
        public IActionResult Create([FromBody] ShipmentChecklistActivities entity)
        {
            _service.Create(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }

        // POST: api/ShipmentChecklistActivities/{id}
        [HttpPost("Update/{id}")]
        public IActionResult Update(string id, [FromBody] ShipmentChecklistActivities entity)
        {
            var existing = _service.GetById(id);
            if (existing == null)
                return NotFound();

            entity.Id = id;
            _service.Update(id, entity);
            return NoContent();
        }
    }
}
