namespace CryptoChat.Api.Contracts.WebRoutes;

public static class RoomControllerWebRoutes
{
    public const string BasePath = "api/rooms";
    public const string GetMessagesFromRoom = "{roomId:int}/messages";
    public const string CreateRoom = "create-room";
    public const string SendMessageToRoom = "{roomId:int}/send-message";
}