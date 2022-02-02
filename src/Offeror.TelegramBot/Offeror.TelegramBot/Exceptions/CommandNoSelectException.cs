using Offeror.TelegramBot.Contracts;

namespace Offeror.TelegramBot.Exceptions
{
    public class CommandNoSelectException : Exception, INotifiableException
    {
        public CommandNoSelectException()
            : this("The command is not selected or the command timeout has expired")
        {
        }

        public CommandNoSelectException(string? message) 
            : base(message)
        {
        }

        public async Task NotifyAsync(INotificationVisitor visitor, long chatId)
        {
            await visitor.VisitAsync(this, chatId);
        }
    }
}
