using Telegram.Bot.Types;

namespace Offeror.TelegramBot.Commands
{
    public interface IBotCommand
    {
        string CommandName { get; }

        bool IsCompleted { get; }

        Task Complete();

        Task InvokeAsync(Update update);
    }
}
