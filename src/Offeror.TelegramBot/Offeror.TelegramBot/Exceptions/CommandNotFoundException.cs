using Offeror.TelegramBot.Contracts;

namespace Offeror.TelegramBot.Exceptions
{
    public class CommandNotFoundException : Exception, INotifiableException
    {
        public CommandNotFoundException()
            : this("The specified command does not exist")
        {
        }

        public CommandNotFoundException(string? message)
            : base(message)
        {
        }

        public async Task NotifyAsync(INotificationVisitor visitor, long chatId)
        {
            await visitor.VisitAsync(this, chatId);
        }
    }
}
