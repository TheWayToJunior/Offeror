using Offeror.TelegramBot.Contracts;
using Offeror.TelegramBot.Extensions;
using Telegram.Bot.Types;

namespace Offeror.TelegramBot.Commands.Start.States
{
    public class KeywordsInputState : IState
    {
        private readonly ISearchFilterWriter _filter;

        public KeywordsInputState(ISearchFilterWriter filter)
        {
            _filter = filter;
        }

        public async Task ExecuteAsync(IBotStateMachine command, Update update)
        {
            var states = command as IStateContainer ?? throw new InvalidCastException();

            IState nextState = update?.Message?.Text switch
            {
                Buttons.Back => states.GetState<RegionDisplayState>(),
                Buttons.Search => ButtonSearchHandle(states),
                Buttons.Clear => ButtonClearKeywordsHandle(states),

                _ => KeywordsHandle(states, update.GetTextMessage()),
            };

            await command.UpdateState(nextState).ExecuteAsync(command, update);
        }

        private IState ButtonSearchHandle(IStateContainer states)
        {
            /// TODO: Check if the search filter is full
            return states.GetState<SearchDisplayState>();
        }

        private IState ButtonClearKeywordsHandle(IStateContainer states)
        {
            _filter.ClearKeyword();
            return states.GetState<KeywordsDisplayState>();
        }

        private IState KeywordsHandle(IStateContainer states, string keywords)
        {
            _filter.AppendKeyword(keywords);
            return states.GetState<KeywordsDisplayState>();
        }
    }
}
