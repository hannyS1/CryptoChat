using CryptoChat.Api.Contracts.Data;
using CryptoChat.AppServices.Contracts.Services;
using CryptoChat.AppServices.Mappers.Interfaces;
using CryptoChat.AppServices.Repositories.Interfaces;
using CryptoChat.Common.Contracts.Exceptions;
using CryptoChat.Entities;

namespace CryptoChat.AppServices.Services;

internal class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserMapper _userMapper;

    public UserService(IUserRepository userRepository, IUserMapper userMapper)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _userMapper = userMapper ?? throw new ArgumentNullException(nameof(userMapper));
    }
    
    public async Task<UserDto> AuthenticateAsync(string name, string password)
    {
        var user = await _userRepository.GetByLoginPasswordAsync(name, password);
        if (user == null)
            throw new ChatException("invalid login or password");
        return _userMapper.Map(user);
    }

    public async Task<UserDto> Create(string name, string password)
    {
        var isUserExist = await _userRepository.GetByLogin(name) != null;
        if (isUserExist)
            throw new ChatException("user already exist");
        
        var user = await _userRepository.AddAsync(new User { Name = name, Password = password });
        await _userRepository.SaveChangesAsync();
        return _userMapper.Map(user);
    }

    public async Task<UserDto> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return _userMapper.Map(user);
    }

    public async Task<List<UserDto>> GetAllExceptOne(int userId)
    {
        var users = await _userRepository.GetAllExceptOne(userId);
        return users.Select(u => _userMapper.Map(u)).ToList();
    }
}