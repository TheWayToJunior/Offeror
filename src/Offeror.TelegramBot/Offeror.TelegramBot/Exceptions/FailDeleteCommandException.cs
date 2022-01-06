namespace Offeror.TelegramBot.Exceptions
{
    public class FailDeleteCommandException : Exception
    {
        public FailDeleteCommandException()
           : this("Failed to free up resources occupied by commands")
        {
        }

        public FailDeleteCommandException(string? message)
            : base(message)
        {
        }
    }
}
