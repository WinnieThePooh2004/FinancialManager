using Shared.DTOs;

namespace FinancialManager.Frontend.Requests
{
    public interface IFinancialOperationsRequests
    {
        Task<FinancialOperationDTO> GetByIdAsync(int id);
        Task<FinancialOperationDTO> DeleteAsync(int id);
        Task<FinancialOperationDTO> CreateAsync(FinancialOperationDTO financialOperation);
        Task<FinancialOperationDTO> UpdateAsync(FinancialOperationDTO financialOperation);
        Task<List<FinancialOperationDTO>> GetAllAsync();
    }
}
