using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinancialManager.Data;
using FinancialManager.Models;
using FinancialManager.DTOs.OperationTypes;
using AutoMapper;

namespace FinancialManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationTypesController : ControllerBase
    {
        private readonly FinancialManagerContext _context;
        private readonly IMapper _mapper;

        public OperationTypesController(FinancialManagerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/OperationTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OperationTypeIndexDto>>> GetOperationType()
        {
            return _mapper.Map<List<OperationTypeIndexDto>>(await _context.OperationTypes.ToListAsync());
        }

        // GET: api/OperationTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OperationTypeDetailsDto>> GetOperationType(int id)
        {
            var operationType = await _context.OperationTypes.FindAsync(id);

            if (operationType == null)
            {
                return NotFound();
            }

            return _mapper.Map<OperationTypeDetailsDto>(operationType);
        }

        // PUT: api/OperationTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOperationType(int id, OperationTypeUpdateDto operationType)
        {
            if (id != operationType.Id)
            {
                return BadRequest();
            }

            var entity = await _context.OperationTypes.FirstOrDefaultAsync(type => type.Id == id);
            if (entity == null)
            {
                return NotFound();
            }
            entity.Name = operationType.Name;
            entity.IsIncome = bool.Parse(operationType.IsIncome);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OperationTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/OperationTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostOperationType(OperationTypeCreateDto operationType)
        {
            var entity = _mapper.Map<OperationType>(operationType);
            _context.OperationTypes.Add(entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/OperationTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperationType(int id)
        {
            var operationType = await _context.OperationTypes.FindAsync(id);
            if (operationType == null)
            {
                return NotFound();
            }

            _context.OperationTypes.Remove(operationType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OperationTypeExists(int id)
        {
            return _context.OperationTypes.Any(e => e.Id == id);
        }
    }
}
