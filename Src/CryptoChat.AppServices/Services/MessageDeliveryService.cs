using CryptoChat.Api.Contracts.Data;
using CryptoChat.AppServices.Contracts.Services;
using CryptoChat.Common.Contracts.Exceptions;
using CryptoChat.Database;
using Microsoft.EntityFrameworkCore;

namespace CryptoChat.AppServices.Services;

public class MessageDeliveryService : IMessageDeliveryService
{
    private readonly IMessageService _messageService;
    private readonly ApplicationContext _dbContext;
    
    public MessageDeliveryService(IMessageService messageService, ApplicationContext dbContext)
    {
        _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
    
    public async Task<MessageDto> SendMessage(SendMessageRequestDto sendMessageRequestDto, int userId, int roomId)
    {
        var room = await _dbContext.Rooms.Where(r => r.Id == roomId).FirstAsync();
        if (room == null)
            throw new ChatException("incorrect room id");
            
        return await _messageService.Create(roomId, userId, sendMessageRequestDto.Text);
    }
}