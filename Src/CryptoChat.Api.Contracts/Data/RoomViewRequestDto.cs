namespace CryptoChat.Api.Contracts.Data;

public class RoomViewDto
{
    public int Id { get; set; }
    public List<UserDto> Users { get; set; }
}