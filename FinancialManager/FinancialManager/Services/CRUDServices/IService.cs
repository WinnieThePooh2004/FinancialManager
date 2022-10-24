namespace FinancialManager.Services.CRUDServices
{
    public interface IService<T> where T : class
    {
        Task AddAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task UpdateAsync(int id, T entity);
        Task DeleteAsync(int id);
    }
}
