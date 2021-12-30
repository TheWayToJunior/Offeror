using Offeror.TelegramBot.Commands.Start.States;
using Telegram.Bot.Types;

namespace Offeror.TelegramBot.Commands
{
    public class StartCommand : IBotCommand, IStateContainer
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IState _defuildState;

        public IState CommandState { get; private set; }

        public StartCommand(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider.CreateScope().ServiceProvider;
            _defuildState = CommandState = GetState<DisplayProfilesState>();
        }

        public string CommandName => Commands.StartCommand;

        public bool IsCompleted { get; private set; }

        public DateTime CommandStartTime { get; private set; }

        public IState UpdateState(IState state)
        {
            return CommandState = state;
        }

        public IState Restart()
        {
            IsCompleted = true;
            return UpdateState(_defuildState);
        }

        public async Task InvokeAsync(Update update)
        {
            CommandStartTime = DateTime.Now;
            await CommandState.ExecuteAsync(this, update);
        }

        public IState GetState<T>() where T : IState
        {
            return _serviceProvider.GetRequiredService<T>();
        }
    }
}
