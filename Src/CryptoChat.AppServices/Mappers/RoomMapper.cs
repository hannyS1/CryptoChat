using CryptoChat.Api.Contracts.Data;
using CryptoChat.AppServices.Mappers.Interfaces;
using CryptoChat.Entities;

namespace CryptoChat.AppServices.Mappers;

internal class RoomMapper : IRoomMapper
{
    private readonly IUserMapper _userMapper;
    
    public RoomMapper(IUserMapper userMapper)
    {
        _userMapper = userMapper ?? throw new ArgumentNullException(nameof(userMapper));
    }
    
    public RoomDto Map(Room room)
    {
        return new RoomDto { Id = room.Id };
    }

    public RoomViewDto MapToView(Room room)
    {
        return new RoomViewDto
        {
            Id = room.Id,
            Users = room.Users.Select(_userMapper.Map).ToList(),
        };
    }
}