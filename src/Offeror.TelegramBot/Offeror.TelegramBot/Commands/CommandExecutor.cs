using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Offeror.TelegramBot.Commands
{
    public sealed class CommandExecutor
    {
        private readonly TelegramBotClient _telegramBot;
        private readonly IDictionary<string, IBotCommand> _commands;

        public static IBotCommand? CurrentCommand { get; set; }

        public CommandExecutor(TelegramBotClient telegramBot)
        {
            _commands = new Dictionary<string, IBotCommand>()
            {
                { Commands.StartCommand, new StartCommand(telegramBot) }
            };

            _telegramBot = telegramBot;
        }

        public async Task ExecuteAsync(Update update)
        {
            var command = update.Message?.Entities?.SingleOrDefault(e => e.Type == MessageEntityType.BotCommand);

            if (command is not null)
            {
                await SetBotCommandAsync(update);
            }

            await (CurrentCommand?.InvokeAsync(update) ?? SendErrorMessageAsync(update, "First you need to specify the command"));
        }

        private async Task SetBotCommandAsync(Update update)
        {
            string? commandKey = update.Message?.Text;

            if (string.IsNullOrWhiteSpace(commandKey) || !_commands.ContainsKey(commandKey))
            {
                await SendErrorMessageAsync(update, "The specified command does not exist");
                return;
            }

            if (!CurrentCommand?.IsCompleted ?? false)
            {
                await SendErrorMessageAsync(update, "Finish the previous command first");
                return;
            }

            CurrentCommand = _commands[commandKey];
        }

        private async Task SendErrorMessageAsync(Update update, string message)
        {
            long? chatId = update?.Message?.Chat.Id;

            if(chatId is null)
            {
                throw new InvalidOperationException("Сhat id not specified");
            }

            await _telegramBot.SendTextMessageAsync(chatId, message);
        }
    }
}
