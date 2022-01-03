using Offeror.TelegramBot.Contracts;
using Offeror.TelegramBot.Extensions;
using Offeror.TelegramBot.Features.Resume;
using Offeror.TelegramBot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Offeror.TelegramBot.Commands.Start.States
{
    public class DisplaySearchState : IState
    {
        private readonly TelegramBotClient _client;
        private readonly ISearchFilterReader _filter;

        public DisplaySearchState(TelegramBotClient client, ISearchFilterReader filter)
        {
            _client = client;
            _filter = filter;
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
            SearchFilter filter = _filter.GetFilter();

            IAnnouncement announcement = new GetResumeResponse() 
            { 
                FirstName = "Miha", 
                LastName = "Smolenskiy", 
                Position = ".NET Developer",
                Experience = "1 year",
                KeySkills = new[] { ".NET", "ASP.NET Core", "EF Core" },
                Contacts = new[] { "+38095914578", "https://github.com/TheWayToJunior" },
                Link = "https://hh.ru/resume/63d915a1ff09634cde0039ed1f654954555936"
            };

            await announcement.AcceptAsync(
                new TelegramDisplayVisitor(_client, chatId, ReplyKeyboardMarkup));

            command.UpdateState(command.Cast<IStateContainer>().GetState<SetSearchState>());
        }
    }
}
