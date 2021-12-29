using Telegram.Bot.Types;

namespace Offeror.TelegramBot.Commands
{
    public interface IBotCommand
    {
        string CommandName { get; }

        bool IsCompleted { get; }

        IState UpdateState(long chatId, IState state);

        IState Restart(long chatId);

        Task InvokeAsync(Update update);
    }

    public interface IStateContainer
    {
        IState GetState<T>() where T : IState;
    }
}
