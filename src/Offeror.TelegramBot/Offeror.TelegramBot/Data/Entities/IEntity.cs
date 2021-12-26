namespace Offeror.TelegramBot.Data.Entities
{
    public interface IEntity<out TKey>
    {
        TKey Id { get; }

        DateTime CreatedAt { get; set; }

        DateTime UpdatedAt { get; set; }
    }
}
