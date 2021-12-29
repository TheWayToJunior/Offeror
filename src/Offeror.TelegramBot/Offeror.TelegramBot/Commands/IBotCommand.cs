using Telegram.Bot.Types;

namespace Offeror.TelegramBot.Commands
{
    public interface IBotCommand
    {
        string CommandName { get; }

        bool IsCompleted { get; }

        IState UpdateState(IState state);

        IState Restart();

        Task InvokeAsync(Update update);
    }

    public interface IStateContainer
    {
        IState GetState<T>() where T : IState;
    }
}
