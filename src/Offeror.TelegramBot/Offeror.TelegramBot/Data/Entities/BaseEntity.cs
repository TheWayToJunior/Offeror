namespace Offeror.TelegramBot.Data.Entities
{
    public class BaseEntity : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
