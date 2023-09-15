namespace CryptoChat.Common.Contracts.Exceptions;

public class ChatException : Exception
{
    public ChatException(string message) : base(message) { }

    public ChatException(string message, Exception exception) : base(message, exception) { }
}