using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoChat.Database.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEntitiesWithEfPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }
        
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connectionString));
        return services;
    }
}