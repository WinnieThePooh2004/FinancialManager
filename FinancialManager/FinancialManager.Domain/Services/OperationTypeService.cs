using AutoMapper;
using FinancialManager.Shared.Exceptions.DomainExceptions;
using FinancialManager.Shared.Models;
using FinancialManager.Shared.Interfaces.Repositiories;
using FinancialManager.Shared.DTOs;
using FinancialManager.Shared.Interfaces.Services;

namespace FinancialManager.Domain.Services
{
    public class OperationTypeService : ICRUDService<OperationTypeDTO>
    {
        private readonly ICRUDRepository<OperationType> _repository;
        private readonly IMapper _mapper;
        public OperationTypeService(ICRUDRepository<OperationType> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OperationTypeDTO> CreateAsync(OperationTypeDTO entity)
        {
            var type = _mapper.Map<OperationType>(entity);
            return _mapper.Map<OperationTypeDTO>(await _repository.CreateAsync(type));
        }

        public async Task<OperationTypeDTO> DeleteAsync(int? id)
        {
            if (id is null)
            {
                throw new NullIdException();
            }
            return _mapper.Map<OperationTypeDTO>(await _repository.DeleteAsync((int)id));
        }

        public async Task<IEnumerable<OperationTypeDTO>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<OperationTypeDTO>>(await _repository.GetAllAsync());
        }

        public async Task<OperationTypeDTO> GetByIdAsync(int? id)
        {
            if (id is null)
            {
                throw new NullIdException();
            }
            return _mapper.Map<OperationTypeDTO>(await _repository.GetByIdAsync((int)id));
        }

        public async Task<OperationTypeDTO> UpdateAsync(OperationTypeDTO entity)
        {
            var type = _mapper.Map<OperationType>(entity);
            return _mapper.Map<OperationTypeDTO>(await _repository.UpdateAsync(type));
        }
    }
}
