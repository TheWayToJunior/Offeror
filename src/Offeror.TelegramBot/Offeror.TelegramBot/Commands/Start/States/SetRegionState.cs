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

            var region = update?.Message?.Text switch
            {
                Buttons.Russia => "rus",
                Buttons.Ukraine => "ukr",

                _ => throw new InvalidOperationException("There is no such answer option"),
            };

            _filter.SetProperty(nameof(SearchFilter.Region), region);
            IState nextState = states.GetState<DisplaySearchState>();

            await command.UpdateState(nextState).ExecuteAsync(command, update);
        }
    }
}
