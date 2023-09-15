using CryptoChat.Api.Contracts.Data;

namespace CryptoChat.AppServices.Contracts.Services;

public interface IMessageService
{
    public Task<List<MessageDto>> GetByRoomIdAsync(int roomId);

    public Task<MessageDto> Create(int roomId, int userId, string text);

    public Task<List<AnnotatedMessageDto>> GetAnnotatedByRoomIdAsync(int roomId, int currentUserId);
}