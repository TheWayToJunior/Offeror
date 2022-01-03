using Offeror.TelegramBot.Contracts;
using Offeror.TelegramBot.Models;
using System.Reflection;
using Telegram.Bot;

namespace Offeror.TelegramBot
{
    internal static class DependencyInjectionExtensions
    {
        internal static IServiceCollection AddTelegramBot(this IServiceCollection services, IConfiguration configuration)
        {
            var token = configuration.GetSection("BotConfiguration").Get<BotConfiguration>().Token;

            if (token == null)
            {
                throw new ArgumentNullException(nameof(token));
            }

            return services.AddScoped(provider => new TelegramBotClient(token));
        }

        internal static IServiceCollection AddBotStates(this IServiceCollection services, Assembly assembly)
        {
            var interfaceType = typeof(IState);

            foreach (Type type in assembly.GetTypes().Where(t => t.IsClass))
            {
                var isImplementing = type.GetInterfaces().Any(@interface => @interface == interfaceType);

                if (isImplementing)
                {
                    services.AddScoped(type);
                }
            }

            return services;
        }

        internal static IServiceCollection AddBotSearchFilter(this IServiceCollection services)
        {
            return services.AddScoped<ISearchFilterBuilder, SearchFilter.SearchFilterBuilder>()
                .AddScoped<ISearchFilterWriter>(p => p.GetRequiredService<ISearchFilterBuilder>())
                .AddScoped<ISearchFilterReader>(p => p.GetRequiredService<ISearchFilterBuilder>());
        }
    }
}
