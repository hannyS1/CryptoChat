using CryptoChat.Api.Contracts.Data;
using CryptoChat.Api.Contracts.WebRoutes;
using CryptoChat.AppServices.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Common.Extensions;

namespace WarehouseManagement.ChatModule;

[ApiController]
[Route(ChatControllerWebRoutes.BasePath)]
public class ChatController : Controller
{
    private readonly HttpContext _httpContext;
    private readonly IMessageService _messageService;
    private readonly IRoomService _roomService;
    private readonly IMessageDeliveryService _messageDeliveryService;
    
    public ChatController(
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
    [HttpGet(ChatControllerWebRoutes.GetMyRooms)]
    public async Task<ActionResult<List<RoomViewDto>>> GetMyRooms()
    {
        var currentUserId = _httpContext.User.RetrieveId();
        return Ok(await _roomService.GetRoomsWithUsersByUserId(currentUserId));
    }
 
    [Authorize]
    [HttpGet(ChatControllerWebRoutes.GetMessagesFromRoom)]
    public async Task<ActionResult<List<AnnotatedMessageDto>>> GetMessages([FromRoute] int roomId)
    {
        var currentUserId = _httpContext.User.RetrieveId();
        var messages = await _messageService.GetAnnotatedByRoomIdAsync(roomId, currentUserId);
        return Ok(messages);
    }

    [Authorize]
    [HttpPost(ChatControllerWebRoutes.CreateRoom)]
    public async Task<ActionResult> Create([FromBody] RoomCreateRequestDto dto)
    {
        var currentUserId = _httpContext.User.RetrieveId();
        await _roomService.CreateAsync(currentUserId, dto.UserId);
        return NoContent();
    }
    
    [Authorize]
    [HttpPost(ChatControllerWebRoutes.SendMessageToRoom)]
    public async Task<ActionResult<MessageDto>> SendMessage([FromRoute] int roomId, [FromBody] SendMessageRequestDto dto)
    {
        var userId = _httpContext.User.RetrieveId();
        var message = await _messageDeliveryService.SendMessage(dto, userId, roomId);
        return Ok(message);
    }   
}