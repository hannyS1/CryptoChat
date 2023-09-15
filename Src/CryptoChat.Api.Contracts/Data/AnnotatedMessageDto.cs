namespace CryptoChat.Api.Contracts.Data;

public class AnnotatedMessageDto
{
    public int Id { get; set; }
    public UserDto User { get; set; }
    public string Text { get; set; }
    public bool IsMyMessage { get; set; }
}