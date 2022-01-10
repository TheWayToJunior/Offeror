using Offeror.TelegramBot.Contracts;
using Offeror.TelegramBot.Extensions;
using Offeror.TelegramBot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Offeror.TelegramBot.Commands
{
    public class ExecutorExceprionHandler : ICommandExecutor
    {
        private readonly ITelegramBotClient _client;

        public ExecutorExceprionHandler(ICommandExecutor commandExecutor, ITelegramBotClient client)
        {
            _client = client;
            CommandExecutor = commandExecutor;
        }

        public ICommandExecutor CommandExecutor { get; }

        public async Task<IEnumerable<KeyValuePair<long, IBotCommand>>> ClearOutdatedCommandsAsync()
        {
            return await CommandExecutor.ClearOutdatedCommandsAsync();
        }

        public async Task ExecuteAsync(Update update)
        {
            try
            {
                await CommandExecutor.ExecuteAsync(update);
            }
            catch (Exception ex)
            {
                if (ex is not INotifiableException notifiable)
                {
                    throw;
                }

                long chatId = update.GetChatId();
                await notifiable.NotifyAsync(new TelegramNotifyExceptionVisitor(_client), chatId);
            }
        }

        public IBotCommand? GetUserCommand(long chatId) => CommandExecutor.GetUserCommand(chatId); 
    }
}
