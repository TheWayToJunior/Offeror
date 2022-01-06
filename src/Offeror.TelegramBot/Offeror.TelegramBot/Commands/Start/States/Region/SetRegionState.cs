using Offeror.TelegramBot.Contracts;
using Offeror.TelegramBot.Exceptions;
using Offeror.TelegramBot.Models;
using Telegram.Bot.Types;

namespace Offeror.TelegramBot.Commands.Start.States
{
    public class SetRegionState : IState
    {
        private readonly ISearchFilterWriter _filter;

        public SetRegionState(ISearchFilterWriter filter)
        {
            _filter = filter;
        }

        public async Task ExecuteAsync(IBotCommand command, Update update)
        {
            var states = command as IStateContainer ?? throw new InvalidCastException();

            IState nextState = update?.Message?.Text switch
            {
                Buttons.Russia  => ButtonNextHandler(states, "rus"),
                Buttons.Ukraine => ButtonNextHandler(states, "ukr"),
                Buttons.Back    => ButtonBackHandler(states),

                _ => throw new CommandNotFoundException("There is no such answer option"),
            };

            await command.UpdateState(nextState).ExecuteAsync(command, update);
        }

        private IState ButtonNextHandler(IStateContainer states, string filter)
        {
            _filter.SetProperty(
                nameof(SearchFilter.Region), filter);

            return states.GetState<DisplaySearchState>();
        }

        private IState ButtonBackHandler(IStateContainer states)
        {
            return states.GetState<DisplayProfilesState>();
        }
    }
}
