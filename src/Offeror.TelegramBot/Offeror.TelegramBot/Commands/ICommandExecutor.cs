using Telegram.Bot.Types;

namespace Offeror.TelegramBot.Commands
{
    public interface ICommandExecutor
    {
        Task ClearOutdatedCommands();

        Task ExecuteAsync(Update update);
    }
}
