using Offeror.TelegramBot.Models;
using Telegram.Bot.Types;

namespace Offeror.TelegramBot.Commands.Start.States
{
    public class SetProfileState : IState
    {
        private readonly SearchFilter _filter;

        public SetProfileState(SearchFilter filter)
        {
            _filter = filter;
        }

        public async Task ExecuteAsync(IBotCommand command, Update update)
        {
            var states = command as IStateContainer ?? throw new InvalidCastException();

            /// TODO : Save data from a message
            _filter.Status = (update?.Message?.Text) switch
            {
                Buttons.Applicant => "applicant",
                Buttons.Сompany   => "company",

                _ => throw new InvalidOperationException("There is no such answer option"),
            };

            long? chatId = update?.Message?.Chat.Id ?? throw new ArgumentNullException(nameof(chatId));
            IState nextState = states.GetState<DisplayRegionsState>();

            await command.UpdateState(chatId.Value, nextState)
                .ExecuteAsync(command, update);
        }
    }
}
