using CryptoChat.AppServices.Repositories.Interfaces;
using CryptoChat.Database;
using CryptoChat.Entities;
using Microsoft.EntityFrameworkCore;

namespace CryptoChat.AppServices.Repositories;

internal class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(ApplicationContext context) : base(context)
    {
    }

    public async Task<User> GetByLoginPasswordAsync(string login, string password)
    {
        return await Items.SingleOrDefaultAsync(u => u.Name == login && u.Password == password);
    }

    public async Task<User> GetByLogin(string login)
    {
        return await Items.SingleOrDefaultAsync(u => u.Name == login);
    }

    public async Task<List<User>> GetAllExceptOne(int userId)
    {
        return await Items.Include(u => u.AllowedCategory).Where(u => u.Id != userId).ToListAsync();
    }
}