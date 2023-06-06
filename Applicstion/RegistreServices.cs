using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

public static class RegisterServices
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR((config) =>
        {
            config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        });
        services.AddHttpContextAccessor();

        return services;
    }
}