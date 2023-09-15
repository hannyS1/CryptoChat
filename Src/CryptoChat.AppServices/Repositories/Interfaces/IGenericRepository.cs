namespace CryptoChat.AppServices.Repositories.Interfaces;

public interface IGenericRepository<T> where T : class
{
    public Task<List<T>> GetAllAsync();
    
    public Task<T> GetByIdAsync(int id);
    
    public void Add(T entity);
    
    public Task<T> AddAsync(T entity);
    
    public void Delete(T entity);
    
    public Task SaveChangesAsync();
}