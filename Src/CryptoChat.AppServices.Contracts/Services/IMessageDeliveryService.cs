using CryptoChat.Api.Contracts.Data;

namespace CryptoChat.AppServices.Contracts.Services;

public interface IMessageDeliveryService
{
    public Task<MessageDto> SendMessage(SendMessageRequestDto sendMessageRequestDto, int userId, int roomId);
}