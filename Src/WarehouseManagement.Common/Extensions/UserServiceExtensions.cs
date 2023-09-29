using CryptoChat.Api.Contracts.Data;
using CryptoChat.AppServices.Contracts.Services;
using Microsoft.AspNetCore.Http;

namespace WarehouseManagement.Common.Extensions;

public static class UserServiceExtensions
{
    public static async Task<UserDto> RetrieveFromHttpContext(this IUserService service, HttpContext context)
    {
        if (context == null)
            return null;
        
        return await service.GetByIdAsync(context.GetUserId());
    }
}