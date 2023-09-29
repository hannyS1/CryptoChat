namespace CryptoChat.Frontend.Common;

public class ClientApplicationException : Exception
{
    public ClientApplicationException(string message) : base(message){}
}