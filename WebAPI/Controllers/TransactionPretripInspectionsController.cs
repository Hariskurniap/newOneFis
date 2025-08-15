using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using YourNamespace.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionPretripInspectionsController : ControllerBase
    {
        private readonly ITransactionPretripInspectionsService _service;

        public TransactionPretripInspectionsController(ITransactionPretripInspectionsService service)
        {
            _service = service;
        }

        [HttpGet]
        [EnableQuery]
        public IActionResult GetOData() => Ok(_service.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var entity = _service.GetById(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] TransactionPretripInspections entity)
        {
            _service.Create(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }

        [HttpPost("update/{id}")]
        public IActionResult Update(string id, [FromBody] TransactionPretripInspections entity)
        {
            var existing = _service.GetById(id);
            if (existing == null) return NotFound();
            _service.Update(id, entity);
            return NoContent();
        }
    }
}
