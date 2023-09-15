using CryptoChat.Api.Contracts.Data;
using CryptoChat.AppServices.Mappers.Interfaces;
using CryptoChat.Entities;

namespace CryptoChat.AppServices.Mappers;

internal class MessageMapper : IMessageMapper
{
    private readonly IRoomMapper _roomMapper;
    private readonly IUserMapper _userMapper;
    
    public MessageMapper(IUserMapper userMapper, IRoomMapper roomMapper)
    {
        _userMapper = userMapper ?? throw new ArgumentNullException(nameof(userMapper));
        _roomMapper = roomMapper ?? throw new ArgumentNullException(nameof(roomMapper));
    }
    
    public MessageDto Map(Message message)
    {
        return new MessageDto
        {
            Id = message.Id,
            Room = _roomMapper.Map(message.Room),
            Text = message.Text,
            User = _userMapper.Map(message.User),
        };
    }

    public AnnotatedMessageDto Map(Message message, int currentUserId)
    {
        return new AnnotatedMessageDto
        {
            Id = message.Id,
            Text = message.Text,
            User = _userMapper.Map(message.User),
            IsMyMessage = message.UserId == currentUserId,
        };
    }
}