using CryptoChat.Api.Contracts.Data;
using CryptoChat.AppServices.Mappers.Interfaces;
using CryptoChat.Entities;

namespace CryptoChat.AppServices.Mappers;

internal class RoomMapper : IRoomMapper
{
    public RoomDto Map(Room room)
    {
        return new RoomDto { Id = room.Id };
    }
}