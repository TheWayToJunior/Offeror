using Telegram.Bot.Types;

namespace Offeror.TelegramBot.Commands.Start.States
{
    public class SetSearchState : IState
    {
        public async Task ExecuteAsync(IBotCommand command, Update update)
        {
            var states = command as IStateContainer ?? throw new InvalidCastException();
            long? chatId = update?.Message?.Chat.Id ?? throw new ArgumentNullException(nameof(chatId));

            IState nextState = (update?.Message?.Text) switch
            {
                Buttons.Next => states.GetState<DisplaySearchState>(),
                Buttons.Restart => command.Restart(),

                _ => throw new InvalidOperationException("There is no such answer option"),
            };

            if(nextState is DisplayRegionsState)
            {
                command.UpdateState(nextState);
            }

            await nextState.ExecuteAsync(command, update);
        }
    }
}
