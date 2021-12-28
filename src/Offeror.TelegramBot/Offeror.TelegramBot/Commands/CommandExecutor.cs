using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Offeror.TelegramBot.Commands
{
    public interface ICommandExecutor
    {
        Task ExecuteAsync(Update update);
    }

    public sealed class CommandExecutor : ICommandExecutor
    {
        private readonly IDictionary<string, IBotCommand> _commands;

        public IBotCommand? CurrentCommand { get; private set; }

        public CommandExecutor(IServiceProvider provider)
        {
            _commands = new Dictionary<string, IBotCommand>()
            {
                { Commands.StartCommand, new StartCommand(provider) },
            };
        }

        public async Task ExecuteAsync(Update update)
        {
            var command = update.Message?.Entities?.SingleOrDefault(e => e.Type == MessageEntityType.BotCommand);

            if (command is not null)
            {
                await SetBotCommandAsync(update);
            }

            await (CurrentCommand?.InvokeAsync(update)
                ?? throw new InvalidOperationException("First you need to specify the command"));
        }

        private async Task SetBotCommandAsync(Update update)
        {
            string? commandKey = update.Message?.Text;

            if (string.IsNullOrWhiteSpace(commandKey) || !_commands.ContainsKey(commandKey))
            {
                throw new InvalidOperationException("The specified command does not exist");
            }

            if (CurrentCommand?.CommandName == commandKey)
            {
                await CurrentCommand.Complete(update.Message.Chat.Id); 
                return;
            }

            //if (!CurrentCommand?.IsCompleted ?? false)
            //{
            //    throw new InvalidOperationException("Finish the previous command first");
            //}

            CurrentCommand = _commands[commandKey];
        }
    }
}
