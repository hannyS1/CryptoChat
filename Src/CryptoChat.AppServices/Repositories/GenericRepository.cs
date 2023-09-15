using CryptoChat.AppServices.Repositories.Interfaces;
using CryptoChat.Database;
using Microsoft.EntityFrameworkCore;

namespace CryptoChat.AppServices.Repositories;

internal class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly ApplicationContext _context;
    protected readonly DbSet<T> Items;

    public GenericRepository(ApplicationContext context)
    {
        _context = context;
        Items = _context.Set<T>();
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await Items.ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await Items.FindAsync(id);
    }

    public void Add(T entity)
    {
        Items.Add(entity);
    }

    public async Task<T> AddAsync(T entity)
    {
        var entityEntry = await Items.AddAsync(entity);
        return entityEntry.Entity;
    }

    public void Delete(T entity)
    {
        Items.Remove(entity);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}