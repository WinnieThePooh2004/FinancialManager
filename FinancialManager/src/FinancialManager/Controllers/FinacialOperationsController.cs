using Microsoft.AspNetCore.Mvc;
using FinancialManager.Shared.Interfaces.Services;
using FinancialManager.Shared.DTOs;

namespace FinancialManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialOperationsController : ControllerBase
    {
        private readonly ICRUDService<FinancialOperationDTO> _service;

        public FinancialOperationsController(ICRUDService<FinancialOperationDTO> service)
        {
            _service = service;
        }

        // GET: api/FinacialOperations
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        // GET: api/FinacialOperations/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            return Ok(entity);
        }

        // PUT: api/FinacialOperations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> Put(FinancialOperationDTO finacialOperation)
        {
            return Ok(await _service.UpdateAsync(finacialOperation));
        }

        // POST: api/FinacialOperations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> Post(FinancialOperationDTO finacialOperation)
        {          
            return Ok(await _service.CreateAsync(finacialOperation));
        }

        // DELETE: api/FinacialOperations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFinacialOperation(int id)
        {          
            return Ok(await _service.DeleteAsync(id));
        }
    }
}
