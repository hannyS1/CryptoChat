namespace CryptoChat.Api.Contracts.WebRoutes;

public static class ChatControllerWebRoutes
{
    public const string BasePath = "api/chat";
    public const string GetMessagesFromRoom = "rooms/{roomId:int}/messages";
    public const string CreateRoom = "rooms/create-room";
    public const string SendMessageToRoom = "rooms/{roomId:int}/send-message";
    public const string GetMyRooms = "rooms";
}