using BaseApi.Handlers.Kernel;
using System.Reflection;

namespace BaseApi.Handlers;

public static class HandlersRegistrator
{
    public static IServiceCollection RegisterHandlers(this IServiceCollection services)
    {
        Assembly.GetAssembly(typeof(HandlerBase))!
            .GetTypes()
            .Where(type => type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(HandlerBase)))
            .ToList()
            .ForEach(handler => services.AddScoped(handler));
        return services;
    }
}
