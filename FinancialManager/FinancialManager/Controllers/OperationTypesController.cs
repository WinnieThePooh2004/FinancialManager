using Microsoft.AspNetCore.Mvc;
using FinancialManager.Models;
using Shared.DTOs.OperationTypes;
using AutoMapper;
using FinancialManager.Services.CRUDServices;

namespace FinancialManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationTypesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IService<OperationType> _service;

        public OperationTypesController(IService<OperationType> service, IMapper mapper)
        {
            _mapper = mapper;
            _service = service;
        }

        // GET: api/OperationTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OperationTypeIndexDto>>> GetOperationType()
        {
            return _mapper.Map<List<OperationTypeIndexDto>>(await _service.GetAllAsync());
        }

        // GET: api/OperationTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OperationTypeDetailsDto>> GetOperationType(int id)
        {
            var entity = await _service.GetAsync(id);
            return _mapper.Map<OperationTypeDetailsDto>(entity);

        }

        // PUT: api/OperationTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOperationType(int id, OperationTypeUpdateDto operationType)
        {
            try
            {
                await _service.UpdateAsync(id, _mapper.Map<OperationType>(operationType));
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

        // POST: api/OperationTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostOperationType(OperationTypeCreateDto operationType)
        {
            try
            {
                await _service.AddAsync(_mapper.Map<OperationType>(operationType));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        // DELETE: api/OperationTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperationType(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
            }
            catch (Exception)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
