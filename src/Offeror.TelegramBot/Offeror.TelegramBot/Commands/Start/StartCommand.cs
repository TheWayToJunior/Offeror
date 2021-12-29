using Offeror.TelegramBot.Commands.Start.States;
using System.Collections.Concurrent;
using Telegram.Bot.Types;

namespace Offeror.TelegramBot.Commands
{
    public class StartCommand : IBotCommand, IStateContainer
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IState _defuildState;

        private readonly ConcurrentDictionary<long, IState> _usersStates;

        public StartCommand(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider.CreateScope().ServiceProvider;
            _defuildState = GetState<DisplayProfilesState>();

            _usersStates = new();
        }

        public string CommandName => Commands.StartCommand;

        public bool IsCompleted { get; private set; }

        public IState UpdateState(long chatId, IState state)
        {
            return _usersStates.AddOrUpdate(chatId, state, (key, oldValue) => state);
        }

        public IState Restart(long chatId)
        {
            IsCompleted = true;
            return UpdateState(chatId, _defuildState);
        }

        public async Task InvokeAsync(Update update)
        {
            long? chatId = update?.Message?.Chat.Id ?? throw new ArgumentNullException(nameof(chatId));

            if(!_usersStates.TryGetValue(chatId.Value, out IState? state))
            {
                state = UpdateState(chatId.Value, _defuildState);
            }

            await state.ExecuteAsync(this, update);
        }

        public IState GetState<T>() where T : IState
        {
            return _serviceProvider.GetRequiredService<T>();
        }
    }
}
