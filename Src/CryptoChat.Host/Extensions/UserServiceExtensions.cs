using CryptoChat.Api.Contracts.Data;
using CryptoChat.AppServices.Contracts.Services;

namespace CryptoChat.Host.Extensions;

public static class UserServiceExtensions
{
    public static async Task<UserDto> RetrieveFromHttpContext(this IUserService service, HttpContext context)
    {
        if (context == null)
            return null;
        
        return await service.GetByIdAsync(context.GetUserId());
    }
}