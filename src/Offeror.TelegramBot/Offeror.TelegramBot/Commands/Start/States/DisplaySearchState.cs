using Offeror.TelegramBot.Extensions;
using Offeror.TelegramBot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Offeror.TelegramBot.Commands.Start.States
{
    public class DisplaySearchState : IState
    {
        private readonly TelegramBotClient _client;

        /// TODO: Fix this
        private readonly SearchFilter filter;

        public DisplaySearchState(TelegramBotClient client, SearchFilter filter)
        {
            _client = client;
            this.filter = filter;
        }

        private static ReplyKeyboardMarkup ReplyKeyboardMarkup =>
            new(
                new[]
                {
                    new KeyboardButton[] { Buttons.Next, Buttons.Restart },
                })
            {
                ResizeKeyboard = true
            };

        public async Task ExecuteAsync(IBotCommand command, Update update)
        {
            long chatId = update.GetChatId();

            /// TODO: Generate a message with a job search response
            await _client.SendTextMessageAsync(chatId, $"{filter.Status} {filter.Region}",
                replyMarkup: ReplyKeyboardMarkup);

            command.UpdateState(((IStateContainer)command).GetState<SetSearchState>());
        }
    }
}
