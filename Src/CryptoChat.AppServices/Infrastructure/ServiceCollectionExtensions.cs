using CryptoChat.AppServices.Contracts.Services;
using CryptoChat.AppServices.Mappers;
using CryptoChat.AppServices.Mappers.Interfaces;
using CryptoChat.AppServices.Repositories;
using CryptoChat.AppServices.Repositories.Interfaces;
using CryptoChat.AppServices.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoChat.AppServices.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddSingleton<IUserMapper, UserMapper>();
        services.AddSingleton<IMessageMapper, MessageMapper>();
        services.AddSingleton<IRoomMapper, RoomMapper>();

        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IRoomService, RoomService>();
        services.AddTransient<IMessageService, MessageService>();
        services.AddTransient<IMessageDeliveryService, MessageDeliveryService>();
        
        return services;
    }
}