using CryptoChat.Api.Contracts.Data;
using CryptoChat.AppServices.Contracts.Services;
using CryptoChat.AppServices.Mappers.Interfaces;
using CryptoChat.Common.Contracts.Exceptions;
using CryptoChat.Database;
using CryptoChat.Entities;
using Microsoft.EntityFrameworkCore;

namespace CryptoChat.AppServices.Services;

internal class MessageService : IMessageService
{
    private readonly ApplicationContext _dbContext;
    private readonly IMessageMapper _messageMapper;
    
    public MessageService(ApplicationContext dbContext, IMessageMapper messageMapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _messageMapper = messageMapper ?? throw new ArgumentNullException(nameof(messageMapper));
    }
    
    public async Task<List<MessageDto>> GetByRoomIdAsync(int roomId)
    {
        var messages = await _dbContext.Messages
            .Where(m => m.RoomId == roomId)
            .Include(m => m.User)
            .Include(m => m.Room)
            .ToListAsync();
        return messages.Select(m => _messageMapper.Map(m)).ToList();
    }
    
    public async Task<List<AnnotatedMessageDto>> GetAnnotatedByRoomIdAsync(int roomId, int currentUserId)
    {
        var isMember = await _dbContext.UsersRooms
            .AnyAsync(ur => ur.UserId == currentUserId && ur.RoomId == roomId);

        if (!isMember)
            throw new ChatException("you're not a member of this room");
            
        var messages = await _dbContext.Messages
            .Where(m => m.RoomId == roomId)
            .Include(m => m.User)
            .ToListAsync();
        
        return messages.Select(m => _messageMapper.Map(m, currentUserId)).ToList();
    }

    public async Task<MessageDto> Create(int roomId, int userId, string text)
    {
        var messageToCreate = new Message { RoomId = roomId, UserId = userId, Text = text };
        var message = await _dbContext.Messages.AddAsync(messageToCreate);
        await _dbContext.SaveChangesAsync();
        return _messageMapper.Map(message.Entity);
    }
}