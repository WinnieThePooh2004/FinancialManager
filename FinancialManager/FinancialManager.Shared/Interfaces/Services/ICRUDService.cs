namespace FinancialManager.Shared.Interfaces.Services
{
    public interface ICRUDService<T> where T : class
    {
        Task<T> CreateAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int? id);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(int? id);
    }
}
