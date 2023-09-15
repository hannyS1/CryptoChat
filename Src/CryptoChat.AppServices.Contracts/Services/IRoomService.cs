namespace CryptoChat.AppServices.Contracts.Services;

public interface IRoomService
{
    public Task CreateAsync(int firstUserId, int secondUserId);
}