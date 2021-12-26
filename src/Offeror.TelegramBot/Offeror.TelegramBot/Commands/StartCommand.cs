using Telegram.Bot;
using Telegram.Bot.Types;

namespace Offeror.TelegramBot.Commands
{
    public class StartCommand : IBotCommand
    {
        private readonly TelegramBotClient _telegramBot;

        public StartCommand(TelegramBotClient telegramBot)
        {
            _telegramBot = telegramBot;
        }

        public string CommandName => Commands.StartCommand;

        public bool IsCompleted { get; private set; }

        public Task Complete()
        {
            IsCompleted = true;
            return Task.CompletedTask;
        }

        public async Task InvokeAsync(Update update)
        {
            long? chatId = update?.Message?.Chat.Id;
            await _telegramBot.SendTextMessageAsync(chatId, "Hello, I'm bot");
        }
    }
}
