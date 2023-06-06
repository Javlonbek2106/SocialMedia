using Application.Abstractions;
using Application.Events;
using LazyCache;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialMedia.Infrustructure.Interceptor;
using Telegram.Bot;

namespace Infrastructure
{
    public static class RegisterServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<InterceptorClass>();
            services.AddScoped<IApplicationDbContext, SocialMediaDbContext>();
            services.AddDbContext<IApplicationDbContext, SocialMediaDbContext>(x => x.UseNpgsql(configuration.GetConnectionString("DbConnection")));
            services.AddSingleton<ITelegramBotClient>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var botToken = configuration["Telegram:BotToken"];
                return new TelegramBotClient(botToken);
            });
            return services;
        }
        
    }
}
