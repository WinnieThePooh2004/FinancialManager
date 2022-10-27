using Microsoft.AspNetCore.Mvc;
using FinancialManager.Models;
using Shared.DTOs.FinancialOperations;
using AutoMapper;
using FinancialManager.Services.CRUDServices;

namespace FinancialManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialOperationsController : ControllerBase
    {
        private readonly IService<FinancialOperation> _service;
        private readonly IMapper _mapper;

        public FinancialOperationsController(IService<FinancialOperation> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/FinacialOperations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FinancialOperationIndexDto>>> GetFinacialOperation()
        {
            try
            {
                return _mapper.Map<List<FinancialOperationIndexDto>>(await _service.GetAllAsync());
            }
            catch (Exception ex) 
            {
                return NotFound(ex.Message);
            }
        }

        // GET: api/FinacialOperations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FinancialOperationDetailsDto>> GetFinacialOperation(int id)
        {
            FinancialOperation entity;
            try
            {
                entity = await _service.GetAsync(id);
            }
            catch (Exception)
            {
                return NotFound();
            }

            return _mapper.Map<FinancialOperationDetailsDto>(entity);
        }

        // PUT: api/FinacialOperations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFinacialOperation(int id, FinancialOperationUpdateDto finacialOperation)
        {
            try
            {
                await _service.UpdateAsync(id, _mapper.Map<FinancialOperation>(finacialOperation));
            }
            catch (Exception ex)
            {
                if(ex.Message == "Not found")
                {
                    return NotFound();
                }
                return BadRequest();
            }
            return NoContent();
        }

        // POST: api/FinacialOperations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostFinacialOperation(FinancialOperationCreateDto finacialOperation)
        {
            try
            {
                await _service.AddAsync(_mapper.Map<FinancialOperation>(finacialOperation));
            }
            catch (Exception) 
            {
                return BadRequest();
            }
            return NoContent();
        }

        // DELETE: api/FinacialOperations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFinacialOperation(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
            }
            catch
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
