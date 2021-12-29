using Telegram.Bot.Types;

namespace Offeror.TelegramBot.Extensions
{
    public static class UpdateExtensions
    {
        public static long GetChatId(this Update update) =>
            update?.Message?.Chat.Id ?? throw new ArgumentNullException(nameof(update.Message));

        public static string GetTextMessage(this Update update) =>
            update?.Message?.Text ?? throw new ArgumentNullException(nameof(update.Message));
    }
}
