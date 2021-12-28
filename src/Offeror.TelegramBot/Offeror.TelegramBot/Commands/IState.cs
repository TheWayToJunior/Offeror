using Telegram.Bot.Types;

namespace Offeror.TelegramBot.Commands
{
    public interface IState
    {
        Task ExecuteAsync(IBotCommand command, Update update);
    }
}
