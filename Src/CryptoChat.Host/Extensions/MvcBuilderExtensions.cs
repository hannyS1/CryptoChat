using System.Reflection;

namespace CryptoChat.Host.Extensions;

public static class MvcBuilderExtensions
{
    public static IMvcBuilder AddModulesControllers(this IMvcBuilder builder, IConfiguration configuration)
    {
        var assembliesNames = configuration.GetSection("ModulesAssemblies").Get<List<string>>();
        var assemblies = assembliesNames.Select(Assembly.Load);

        foreach (var assembly in assemblies)
        {
            builder.AddApplicationPart(assembly);
        }

        return builder;
    }
}