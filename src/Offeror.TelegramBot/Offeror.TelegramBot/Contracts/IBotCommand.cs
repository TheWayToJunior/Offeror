using Telegram.Bot.Types;

namespace Offeror.TelegramBot.Contracts
{
    public interface ICompletable<T>
    {
        event EventHandler<T>? CommandCompleted;

        bool IsCompleted { get; }
    }

    public interface IBotCommand : ICompletable<long>
    {
        string CommandName { get; }

        DateTime CommandStartTime { get; }

        Task InvokeAsync(Update update);
    }

    public interface IBotStateMachine : IBotCommand
    {
        IState UpdateState(IState state);

        IState Restart();
    }
}
