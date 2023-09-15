using CryptoChat.Api.Contracts.Data;
using CryptoChat.Entities;

namespace CryptoChat.AppServices.Mappers.Interfaces;

public interface IUserMapper
{
    public UserDto Map(User user);
}