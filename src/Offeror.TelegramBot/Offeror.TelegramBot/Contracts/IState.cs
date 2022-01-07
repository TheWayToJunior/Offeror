using Telegram.Bot.Types;

namespace Offeror.TelegramBot.Contracts
{
    public interface IState
    {
        Task ExecuteAsync(IBotStateMachine command, Update update);
    }
}
