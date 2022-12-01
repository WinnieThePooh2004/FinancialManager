namespace FinancialManager.Shared.Interfaces.Repositiories
{
    public interface ICRUDRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> DeleteAsync(int id);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
    }
}
