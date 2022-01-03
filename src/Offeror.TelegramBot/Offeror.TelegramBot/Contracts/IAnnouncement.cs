namespace Offeror.TelegramBot.Contracts
{
    public interface IAnnouncement
    {
        Task AcceptAsync(IVisitor visitor);
    }
}
