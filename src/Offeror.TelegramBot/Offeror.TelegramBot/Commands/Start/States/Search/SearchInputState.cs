using Offeror.TelegramBot.Contracts;
using Offeror.TelegramBot.Exceptions;
using Telegram.Bot.Types;

namespace Offeror.TelegramBot.Commands.Start.States
{
    public class SearchInputState : IState
    {
        public async Task ExecuteAsync(IBotStateMachine command, Update update)
        {
            var states = command as IStateContainer ?? throw new InvalidCastException();

            IState nextState = update?.Message?.Text switch
            {
                Buttons.Search => states.GetState<SearchDisplayState>(),
                Buttons.Back => states.GetState<KeywordsDisplayState>(),
                Buttons.Restart => command.Restart(),

                _ => throw new CommandNotFoundException("There is no such answer option"),
            };

            if(nextState is SearchDisplayState)
            {
                command.UpdateState(nextState);
            }

            await nextState.ExecuteAsync(command, update);
        }
    }
}
