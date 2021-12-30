using Telegram.Bot.Types;

namespace Offeror.TelegramBot.Commands
{
    public interface ICommandExecutor
    {
        /// <summary>
        /// Deleted commands whose lifetime has expired
        /// </summary>
        /// <returns>A collection of deleted commands and the chat id that the command belongs to</returns>
        Task<IEnumerable<KeyValuePair<long, IBotCommand>>> ClearOutdatedCommandsAsync();

        Task ExecuteAsync(Update update);
    }
}
