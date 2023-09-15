namespace CryptoChat.Api.Contracts.Data;

public class MessageDto
{
    public int Id { get; set; }
    public string Text { get; set; }

    public UserDto User { get; set; }
    public RoomDto Room { get; set; }
}