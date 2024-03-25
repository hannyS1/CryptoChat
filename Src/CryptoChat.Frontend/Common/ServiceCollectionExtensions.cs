using CryptoChat.Frontend.Services;

namespace CryptoChat.Frontend.Common;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddScoped<UserService>();
        services.AddScoped<AuthenticateService>();
        services.AddScoped<AuthTokenProvider>();
        services.AddScoped<WarehouseService>();
        services.AddScoped<FeatureToggleService>();
        services.AddScoped<ChatService>();
        return services;
    }
}