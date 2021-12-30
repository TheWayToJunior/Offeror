using Telegram.Bot.Types;

namespace Offeror.TelegramBot.Commands
{
    public interface IBotCommand
    {
        event EventHandler<long>? CommandCompleted;

        bool IsCompleted { get; }

        string CommandName { get; }

        DateTime CommandStartTime { get; }

        IState UpdateState(IState state);

        IState Restart();

        Task InvokeAsync(Update update);
    }

    public interface IStateContainer
    {
        IState GetState<T>() where T : IState;
    }
}
