using Offeror.TelegramBot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Offeror.TelegramBot.Commands.Start.States
{
    public class SearchState : IState
    {
        private readonly TelegramBotClient _client;

        /// TODO: Fix this
        private readonly SearchFilter filter;

        public SearchState(TelegramBotClient client, SearchFilter filter)
        {
            _client = client;
            this.filter = filter;
        }

        public async Task ExecuteAsync(IBotCommand command, Update update)
        {
            long? chatId = update?.Message?.Chat.Id;

            if (chatId is null)
            {
                throw new ArgumentNullException(nameof(chatId));
            }

            await _client.SendTextMessageAsync(chatId, $"{filter.Status} {filter.Region}",
                replyMarkup: new ReplyKeyboardRemove());
        }
    }
}
