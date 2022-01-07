using MediatR;
using Offeror.TelegramBot.Contracts;
using Offeror.TelegramBot.Extensions;
using Offeror.TelegramBot.Features;
using Offeror.TelegramBot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Offeror.TelegramBot.Commands.Start.States
{
    public class SearchDisplayState : IState
    {
        private readonly ITelegramBotClient _client;
        private readonly ISearchFilterReader _filter;
        private readonly IMediator _mediator;

        public SearchDisplayState(ITelegramBotClient client, ISearchFilterReader filter, IMediator mediator)
        {
            _client = client;
            _filter = filter;
            _mediator = mediator;
        }

        private static ReplyKeyboardMarkup ReplyKeyboardMarkup =>
            new(
                new[]
                {
                    new KeyboardButton[] { Buttons.Search, Buttons.Restart, Buttons.Back },
                })
            {
                ResizeKeyboard = true
            };

        public async Task ExecuteAsync(IBotCommand command, Update update)
        {
            long chatId = update.GetChatId();

            SearchFilter filter = _filter.GetFilter();
            QueryFactory factory = new();

            object query = factory.CreateRequest(filter);
            object? response = await _mediator.Send(query);

            if (response is null)
            {
                throw new InvalidOperationException();
            }

            await response.Cast<IAnnouncement>()
                .AcceptAsync(new TelegramDisplayVisitor(_client, chatId, ReplyKeyboardMarkup));

            command.UpdateState(command.Cast<IStateContainer>().GetState<SearchInputState>());
        }
    }
}
