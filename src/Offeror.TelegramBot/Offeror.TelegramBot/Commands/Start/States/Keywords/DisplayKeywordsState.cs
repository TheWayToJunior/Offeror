using Offeror.TelegramBot.Contracts;
using Offeror.TelegramBot.Extensions;
using Offeror.TelegramBot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Offeror.TelegramBot.Commands.Start.States
{
    public class DisplayKeywordsState : IState
    {
        private readonly ITelegramBotClient _client;
        private readonly ISearchFilterReader _filterReader;

        public DisplayKeywordsState(ITelegramBotClient client, ISearchFilterReader searchFilter)
        {
            _client = client;
            _filterReader = searchFilter;
        }

        private static ReplyKeyboardMarkup ReplyKeyboardMarkup =>
            new(
                new[]
                {
                    new KeyboardButton[] { "C#", "C++", "Java", "Backend", "Frontend" },
                    new KeyboardButton[] { Buttons.Search, Buttons.Clear, Buttons.Back },
                })
            {
                ResizeKeyboard = true
            };

        public async Task ExecuteAsync(IBotCommand command, Update update)
        {
            long chatId = update.GetChatId();
            SearchFilter searchFilter = _filterReader.GetFilter();

            string message = searchFilter.Keywords.Any() switch
            {
                true => $"Filters: {string.Join(", ", searchFilter.Keywords)}",
                false => "Enter the keywords to search for. For example, the name of the programming language..."
            };

            await _client.SendTextMessageAsync(chatId, message, replyMarkup: ReplyKeyboardMarkup);

            command.UpdateState(command.Cast<IStateContainer>().GetState<SetKeywordsState>());
        }
    }
}
