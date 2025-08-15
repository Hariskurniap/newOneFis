using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShipmentValidationsController : ControllerBase
    {
        private readonly IShipmentValidationsService _service;

        public ShipmentValidationsController(IShipmentValidationsService service)
        {
            _service = service;
        }

        [HttpGet]
        [EnableQuery]
        public IActionResult GetOData()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var entity = _service.GetById(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        [HttpPost("Modify")]
        public IActionResult Create([FromBody] ShipmentValidations entity)
        {
            _service.Create(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }

        [HttpPost("Update/{id}")]
        public IActionResult Update(string id, [FromBody] ShipmentValidations entity)
        {
            var existing = _service.GetById(id);
            if (existing == null) return NotFound();
            _service.Update(id, entity);
            return NoContent();
        }
    }
}
