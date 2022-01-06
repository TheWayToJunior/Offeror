using Offeror.TelegramBot.Exceptions;

namespace Offeror.TelegramBot.Contracts
{
    public interface INotifiableException
    {
        Task NotifyAsync(INotificationVisitor visitor, long chatId);
    }

    public interface INotificationVisitor 
    {
        Task VisitAsync(CommandNoSelectException exception, long chatId);

        Task VisitAsync(CommandNotFoundException exception, long chatId);
    }
}
