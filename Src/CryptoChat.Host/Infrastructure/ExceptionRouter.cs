using CryptoChat.Common.Contracts.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CryptoChat.Host.Infrastructure;

public static class ExceptionRouter
{
    public static ApiResult Route(Exception exception)
    {
        if (exception is ChatException)
        {
            return new ApiResult { StatusCode = StatusCodes.Status400BadRequest, ContentType = "text" };
        }

        throw exception;
    }
}