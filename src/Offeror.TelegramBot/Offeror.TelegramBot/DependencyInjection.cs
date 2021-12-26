using Telegram.Bot;

namespace Offeror.TelegramBot
{
    internal static class DependencyInjectionExtensions
    {
        internal static IServiceCollection AddTelegramBot(this IServiceCollection services, IConfiguration configuration)
        {
            var token = configuration.GetSection("BotConfiguration").Get<BotConfiguration>().Token;
            return services.AddScoped(provider => new TelegramBotClient(token));
        }
    }
}
