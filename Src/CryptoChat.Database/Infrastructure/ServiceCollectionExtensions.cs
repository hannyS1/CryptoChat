using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoChat.Database.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEntitiesWithEfSqlite(this IServiceCollection services, IConfiguration configuration)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }
        
        var connectionString = configuration.GetConnectionString("SqliteConnection");
        services.AddDbContext<ApplicationContext>(options => options.UseSqlite(
            connectionString, b => b.MigrationsAssembly("CryptoChat.Host")));
        
        return services;
    }
}