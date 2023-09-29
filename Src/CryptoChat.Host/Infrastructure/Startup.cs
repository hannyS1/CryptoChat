using System.Reflection;
using CryptoChat.AppServices.Infrastructure;
using CryptoChat.Common.Infrastructure;
using CryptoChat.Database.Infrastructure;
using CryptoChat.Entities;
using CryptoChat.Host.Extensions;
using CryptoChat.Host.Middlewares;
using DotNetEd.CoreAdmin;
using Microsoft.OpenApi.Models;

namespace CryptoChat.Host.Infrastructure;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers().AddModulesControllers(_configuration);
        services.AddHttpContextAccessor();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Chat web API", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });
        });

        services.AddAppServices();
        services.AddEntitiesWithEfSqlite(_configuration);
        services.AddJwtAuthentication(_configuration);
        services.AddPasswordHasher();
        services.AddCoreAdmin(new CoreAdminOptions { IgnoreEntityTypes = new List<Type>() { typeof(UserRoom), typeof(Room), typeof(Message) }} );
    }
    
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseMiddleware<JwtMiddleware>();
        
        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseCors(builder => builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .WithExposedHeaders("*"));
        
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
        });
    }
}