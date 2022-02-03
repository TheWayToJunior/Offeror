using Offeror.TelegramBot.Contracts;
using Offeror.TelegramBot.Exceptions;
using Offeror.TelegramBot.Models;
using Telegram.Bot.Types;

namespace Offeror.TelegramBot.Commands.Start.States
{
    public class RegionInputState : IState
    {
        private readonly ISearchFilterWriter _filter;

        public RegionInputState(ISearchFilterWriter filter)
        {
            _filter = filter;
        }

        public async Task ExecuteAsync(IBotStateMachine command, Update update)
        {
            var states = command as IStateContainer ?? throw new InvalidCastException();

            IState nextState = update?.Message?.Text switch
            {
                Buttons.Russia  => ButtonNexHandle(states, "rus"),
                Buttons.Ukraine => ButtonNexHandle(states, "ukr"),
                Buttons.Back    => ButtonBackHandle(states),

                _ => throw new CommandNotFoundException("There is no such answer option"),
            };

            await command.UpdateState(nextState).ExecuteAsync(command, update);
        }

        private IState ButtonNexHandle(IStateContainer states, string filter)
        {
            _filter.SetProperty(
                nameof(SearchFilter.Region), filter);

            return states.GetState<KeywordsDisplayState>();
        }

        private IState ButtonBackHandle(IStateContainer states)
        {
            return states.GetState<ProfileDisplayState>();
        }
    }
}
