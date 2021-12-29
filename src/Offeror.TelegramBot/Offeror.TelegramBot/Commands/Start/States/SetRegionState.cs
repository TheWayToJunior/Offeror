using Offeror.TelegramBot.Models;
using Telegram.Bot.Types;

namespace Offeror.TelegramBot.Commands.Start.States
{
    public class SetRegionState : IState
    {
        private readonly SearchFilter _filter;

        public SetRegionState(SearchFilter filter)
        {
            _filter = filter;
        }

        public async Task ExecuteAsync(IBotCommand command, Update update)
        {
            var states = command as IStateContainer ?? throw new InvalidCastException();

            /// TODO : Save data from a message
            _filter.Region = (update?.Message?.Text) switch
            {
                Buttons.Russia => "rus",
                Buttons.Ukraine => "ukr",

                _ => throw new InvalidOperationException("There is no such answer option"),
            };

            long? chatId = update?.Message?.Chat.Id ?? throw new ArgumentNullException(nameof(chatId));
            IState nextState = states.GetState<DisplaySearchState>();

            await command.UpdateState(chatId.Value, nextState)
                .ExecuteAsync(command, update);
        }
    }
}
