using CryptoChat.Api.Contracts.Data;
using CryptoChat.Api.Contracts.WebRoutes;
using CryptoChat.AppServices.Contracts.Services;
using CryptoChat.Host.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoChat.Host.Controllers;

[Route(RoomControllerWebRoutes.BasePath)]
public class RoomController : Controller
{
    private readonly HttpContext _httpContext;
    private readonly IMessageService _messageService;
    private readonly IRoomService _roomService;
    private readonly IMessageDeliveryService _messageDeliveryService;
    
    public RoomController(
        IHttpContextAccessor httpContextAccessor, 
        IMessageService messageService, 
        IRoomService roomService, 
        IMessageDeliveryService messageDeliveryService)
    {
        _httpContext = httpContextAccessor?.HttpContext ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _roomService = roomService ?? throw new ArgumentNullException(nameof(roomService));
        _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
        _messageDeliveryService =
            messageDeliveryService ?? throw new ArgumentNullException(nameof(messageDeliveryService));
    }

    [Authorize]
    [HttpGet(RoomControllerWebRoutes.GetMessagesFromRoom)]
    public async Task<ActionResult<List<AnnotatedMessageDto>>> GetMessages([FromRoute] int roomId)
    {
        var currentUserId = _httpContext.User.RetrieveId();
        var messages = await _messageService.GetAnnotatedByRoomIdAsync(roomId, currentUserId);
        return Ok(messages);
    }

    [Authorize]
    [HttpPost(RoomControllerWebRoutes.CreateRoom)]
    public async Task<ActionResult> Create([FromBody] RoomCreateRequestDto dto)
    {
        var currentUserId = _httpContext.User.RetrieveId();
        await _roomService.CreateAsync(currentUserId, dto.UserId);
        return NoContent();
    }
    
    [Authorize]
    [HttpPost(RoomControllerWebRoutes.SendMessageToRoom)]
    public async Task<ActionResult<MessageDto>> SendMessage([FromRoute] int roomId, [FromBody] SendMessageRequestDto dto)
    {
        var userId = _httpContext.User.RetrieveId();
        var message = await _messageDeliveryService.SendMessage(dto, userId, roomId);
        return Ok(message);
    }   
}