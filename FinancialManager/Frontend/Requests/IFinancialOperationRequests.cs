using FinancialManager.Shared.DTOs;

namespace Frontend.Requests
{
    public interface IFinancialOperationRequests
    {
        Task<FinancialOperationDTO> CreateAsync(FinancialOperationDTO operation);
        Task<FinancialOperationDTO> DeleteAsync(int? id);
        Task<List<FinancialOperationDTO>> GetAllAsync();
        Task<FinancialOperationDTO> GetByIdAsync(int? id);
        Task<FinancialOperationDTO> UpdateAsync(FinancialOperationDTO operation);
    }
}