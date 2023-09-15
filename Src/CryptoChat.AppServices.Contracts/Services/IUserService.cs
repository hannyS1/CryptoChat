using CryptoChat.Api.Contracts.Data;

namespace CryptoChat.AppServices.Contracts.Services;

public interface IUserService
{
    public Task<UserDto> AuthenticateAsync(string name, string password);

    public Task<UserDto> Create(string name, string password);
    
    public Task<UserDto> GetByIdAsync(int id);

    public Task<List<UserDto>> GetAllExceptOne(int userId);
}