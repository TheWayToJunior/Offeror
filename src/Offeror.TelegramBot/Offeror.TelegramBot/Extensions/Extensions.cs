using Telegram.Bot.Types;

namespace Offeror.TelegramBot.Extensions
{
    public static class Extensions
    {
        public static long GetChatId(this Update update) =>
            update?.Message?.Chat.Id ?? throw new ArgumentNullException(nameof(update.Message));

        public static string GetTextMessage(this Update update) =>
            update?.Message?.Text ?? throw new ArgumentNullException(nameof(update.Message));

        public static T Cast<T>(this object value) where T : class
        {
            if (value is T cast)
            {
                return cast;
            }

            throw new InvalidCastException(nameof(cast));
        }
    }
}
