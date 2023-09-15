using CryptoChat.Api.Contracts.Data;
using CryptoChat.AppServices.Mappers.Interfaces;
using CryptoChat.Entities;

namespace CryptoChat.AppServices.Mappers;

public class UserMapper : IUserMapper
{
    public UserDto Map(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
        };
    }
}