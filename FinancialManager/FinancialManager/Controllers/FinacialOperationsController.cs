using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinancialManager.Data;
using FinancialManager.Models;
using FinancialManager.DTOs.FinancialOperations;
using AutoMapper;

namespace FinancialManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinacialOperationsController : ControllerBase
    {
        private readonly FinancialManagerContext _context;
        private readonly IMapper _mapper;

        public FinacialOperationsController(FinancialManagerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/FinacialOperations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FinancialOperationIndexDto>>> GetFinacialOperation()
        {
            return _mapper.Map<List<FinancialOperationIndexDto>>(await _context.FinacialOperations.ToListAsync());
        }

        // GET: api/FinacialOperations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FinancialOperationDetailsDto>> GetFinacialOperation(int id)
        {
            var entity = await _context.FinacialOperations.FindAsync(id);

            if (entity == null)
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
            if (id != finacialOperation.Id)
            {
                return BadRequest();
            }
            var entity = await _context.FinacialOperations.FirstOrDefaultAsync(operation => operation.Id == id);
            if (entity == null) 
            {
                return NotFound();
            }
            if (!_context.OperationTypes.Any(type => type.Id == entity.OperationTypeId))
            {
                throw new Exception("Can`t find any operation type with same id");
            }

            entity.DateTime = finacialOperation.DateTime;
            entity.Description = finacialOperation.Description;
            entity.OperationTypeId = finacialOperation.OperationTypeId;
            entity.Amount = (int)(double.Parse(finacialOperation.Amount) * 100);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FinacialOperationExists(id))
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

        // POST: api/FinacialOperations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostFinacialOperation(FinancialOperationCreateDto finacialOperation)
        {
            var entity = _mapper.Map<FinacialOperation>(finacialOperation);
            if(!_context.OperationTypes.Any(type => type.Id == entity.OperationTypeId))
            {
                throw new Exception("Can`t find any operation type with same id");
            }
            _context.FinacialOperations.Add(entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/FinacialOperations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFinacialOperation(int id)
        {
            var finacialOperation = await _context.FinacialOperations.FindAsync(id);
            if (finacialOperation == null)
            {
                return NotFound();
            }

            _context.FinacialOperations.Remove(finacialOperation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FinacialOperationExists(int id)
        {
            return _context.FinacialOperations.Any(e => e.Id == id);
        }
    }
}
