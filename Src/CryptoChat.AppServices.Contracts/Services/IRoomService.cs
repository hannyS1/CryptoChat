using CryptoChat.Api.Contracts.Data;

namespace CryptoChat.AppServices.Contracts.Services;

public interface IRoomService
{
    public Task CreateAsync(int firstUserId, int secondUserId);

    public Task<List<RoomViewDto>> GetRoomsWithUsersByUserId(int userId);
}