using Offeror.TelegramBot.Commands.Start.States;
using Offeror.TelegramBot.Contracts;
using Telegram.Bot.Types;

namespace Offeror.TelegramBot.Commands
{
    public class StartCommand : IBotCommand, IStateContainer
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IState _defuildState;

        public StartCommand(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider.CreateScope().ServiceProvider;
            _defuildState = CommandState = GetState<DisplayProfilesState>();
        }

        public event EventHandler<long>? CommandCompleted;

        public bool IsCompleted => false;

        public IState CommandState { get; private set; }

        public string CommandName => BotCommands.StartCommand;

        public DateTime CommandStartTime { get; private set; }

        public IState UpdateState(IState state)
        {
            return CommandState = state;
        }

        public IState Restart()
        {
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
