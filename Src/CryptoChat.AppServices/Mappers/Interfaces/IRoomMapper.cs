using CryptoChat.Api.Contracts.Data;
using CryptoChat.Entities;

namespace CryptoChat.AppServices.Mappers.Interfaces;

public interface IRoomMapper
{
    public RoomDto Map(Room room);
}