using Offeror.TelegramBot.Contracts;
using Offeror.TelegramBot.Exceptions;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace Offeror.TelegramBot.Models
{
    public class TelegramNotifyExceptionVisitor : INotificationVisitor
    {
        private readonly ITelegramBotClient _client;

        public TelegramNotifyExceptionVisitor(ITelegramBotClient client)
        {
            _client = client;
        }

        private static ReplyKeyboardMarkup ReplyKeyboardMarkup =>
            new(
                new[]
                {
                    new KeyboardButton[] { Buttons.Start }
                })
            {
                ResizeKeyboard = true
            };

        public async Task VisitAsync(CommandNoSelectException exception, long chatId)
        {
            await _client.SendTextMessageAsync(chatId, exception.Message, replyMarkup: ReplyKeyboardMarkup);
        }

        public async Task VisitAsync(CommandNotFoundException exception, long chatId)
        {
            await _client.SendTextMessageAsync(chatId, exception.Message);
        }
    }
}
