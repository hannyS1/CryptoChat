using Microsoft.AspNetCore.Http;

namespace WarehouseManagement.Common.Extensions;

public static class HttpContextExtensions
{
    public static int GetUserId(this HttpContext context)
    {
        return context.User.RetrieveId();
    }
}