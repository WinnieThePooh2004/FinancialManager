using FinancialManager.Shared.Interfaces.Repositiories;
using FinancialManager.Shared.Exceptions.DomainExceptions;
using FinancialManager.Shared.DTOs;
using AutoMapper;
using FinancialManager.Shared.Models;
using FinancialManager.Shared.Interfaces.Services;

namespace FinancialManager.Domain.Services
{
    public class FinancialOperationService : ICRUDService<FinancialOperationDTO>
    {
        private readonly ICRUDRepository<FinancialOperation> _repository;
        private readonly IMapper _mapper;
        public FinancialOperationService(ICRUDRepository<FinancialOperation> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<FinancialOperationDTO> CreateAsync(FinancialOperationDTO entity)
        {
            var type = await _repository.CreateAsync(_mapper.Map<FinancialOperation>(entity));
            return _mapper.Map<FinancialOperationDTO>(entity);
        }

        public async Task<FinancialOperationDTO> DeleteAsync(int? id)
        {
            if (id is null)
            {
                throw new NullIdException();
            }
            return _mapper.Map<FinancialOperationDTO>(await _repository.DeleteAsync((int)id));
        }

        public async Task<IEnumerable<FinancialOperationDTO>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<FinancialOperationDTO>>(await _repository.GetAllAsync());
        }

        public async Task<FinancialOperationDTO> GetByIdAsync(int? id)
        {
            if (id is null)
            {
                throw new NullIdException();
            }
            return _mapper.Map<FinancialOperationDTO>(await _repository.GetByIdAsync((int)id));
        }

        public async Task<FinancialOperationDTO> UpdateAsync(FinancialOperationDTO entity)
        {
            var updatedOperation = await _repository.UpdateAsync(_mapper.Map<FinancialOperation>(entity));
            return _mapper.Map<FinancialOperationDTO>(updatedOperation);
        }
    }
}
