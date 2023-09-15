using CryptoChat.Api.Contracts.Data;
using CryptoChat.Entities;

namespace CryptoChat.AppServices.Mappers.Interfaces;

public interface IMessageMapper
{
    public MessageDto Map(Message message);

    public AnnotatedMessageDto Map(Message message, int currentUserId);
}