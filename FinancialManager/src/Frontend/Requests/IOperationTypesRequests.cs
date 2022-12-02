using FinancialManager.Shared.DTOs;

namespace Frontend.Requests
{
    public interface IOperationTypesRequests
    {
        Task<OperationTypeDTO> CreateAsync(OperationTypeDTO operation);
        Task<OperationTypeDTO> DeleteAsync(int? id);
        Task<List<OperationTypeDTO>> GetAllAsync();
        Task<OperationTypeDTO> GetByIdAsync(int? id);
        Task<OperationTypeDTO> UpdateAsync(OperationTypeDTO operation);
    }
}