using Telegram.Bot.Types;

namespace Offeror.TelegramBot.Contracts
{
    public interface IState
    {
        Task ExecuteAsync(IBotCommand command, Update update);
    }
}
