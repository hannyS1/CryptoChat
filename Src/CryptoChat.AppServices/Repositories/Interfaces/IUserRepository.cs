using CryptoChat.Entities;

namespace CryptoChat.AppServices.Repositories.Interfaces;

internal interface IUserRepository : IGenericRepository<User>
{
    public Task<User> GetByLoginPasswordAsync(string login, string password);

    public Task<User> GetByLogin(string login);

    public Task<List<User>> GetAllExceptOne(int userId);
}