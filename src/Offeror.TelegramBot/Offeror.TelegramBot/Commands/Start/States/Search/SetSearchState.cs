using Offeror.TelegramBot.Contracts;
using Offeror.TelegramBot.Exceptions;
using Telegram.Bot.Types;

namespace Offeror.TelegramBot.Commands.Start.States
{
    public class SetSearchState : IState
    {
        public async Task ExecuteAsync(IBotCommand command, Update update)
        {
            var states = command as IStateContainer ?? throw new InvalidCastException();

            IState nextState = update?.Message?.Text switch
            {
                Buttons.Search => states.GetState<DisplaySearchState>(),
                Buttons.Back => states.GetState<DisplayKeywordsState>(),
                Buttons.Restart => command.Restart(),

                _ => throw new CommandNotFoundException("There is no such answer option"),
            };

            if(nextState is DisplaySearchState)
            {
                command.UpdateState(nextState);
            }

            await nextState.ExecuteAsync(command, update);
        }
    }
}
