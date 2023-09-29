namespace CryptoChat.Frontend.Common;

public class ApiException : Exception
{
    public int StatusCode { get; private set; }

    public ApiException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
}