using Offeror.TelegramBot.Models;
using Telegram.Bot.Types;

namespace Offeror.TelegramBot.Commands.Start.States
{
    public class SetProfileState : IState
    {
        private readonly ISearchFilterWriter _filter;

        public SetProfileState(ISearchFilterWriter filter)
        {
            _filter = filter;
        }

        public async Task ExecuteAsync(IBotCommand command, Update update)
        {
            var states = command as IStateContainer ?? throw new InvalidCastException();

            var status = update?.Message?.Text switch
            {
                Buttons.Applicant => "applicant",
                Buttons.Сompany => "company",

                _ => throw new InvalidOperationException("There is no such answer option"),
            };

            _filter.SetProperty(nameof(SearchFilter.Status), status);
            IState nextState = states.GetState<DisplayRegionsState>();

            await command.UpdateState(nextState).ExecuteAsync(command, update);
        }
    }
}
