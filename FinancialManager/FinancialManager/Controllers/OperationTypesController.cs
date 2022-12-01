using Microsoft.AspNetCore.Mvc;
using FinancialManager.Shared.DTOs;
using FinancialManager.Shared.Interfaces.Services;

namespace FinancialManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationTypesController : ControllerBase
    {
        private readonly ICRUDService<OperationTypeDTO> _service;

        public OperationTypesController(ICRUDService<OperationTypeDTO> service)
        {
            _service = service;
        }

        // GET: api/OperationTypes
        [HttpGet]
        public async Task<IActionResult> GetOperationType()
        {
            return Ok(await _service.GetAllAsync());
        }

        // GET: api/OperationTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOperationType(int? id)
        {
            var entity = await _service.GetByIdAsync(id);
            return Ok(entity);
        }

        // PUT: api/OperationTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutOperationType(OperationTypeDTO operationType)
        {
            return Ok(await _service.UpdateAsync(operationType));
        }

        // POST: api/OperationTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostOperationType(OperationTypeDTO operationType)
        {
            return Ok(await _service.CreateAsync(operationType));
        }

        // DELETE: api/OperationTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperationType(int? id)
        {
            return Ok(await _service.DeleteAsync(id));
        }
    }
}
