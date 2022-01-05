using Telegram.Bot.Types;

namespace Offeror.TelegramBot.Tests.Mocks
{
    internal class UpdateFactory
    {
        public static Update CreateUpdate(long chatId, string text) =>
            new() 
            {
                Message = new Message 
                {
                    Text = text,

                    Chat = new Chat 
                    { 
                        Id = chatId 
                    }
                } 
            };
    }
}
