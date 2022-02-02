using Offeror.TelegramBot.Commands;
using Offeror.TelegramBot.Tests.Mocks;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Offeror.TelegramBot.Tests
{
    public class CommandExecutorTests
    {
        [Fact]
        public async Task ExecuteAsync_StopCommand()
        {
            long chatId = 1;
            Update update = UpdateFactory.CreateUpdate(chatId, BotCommands.StopCommand);
            update.Message.Entities = new[] 
            {
                new MessageEntity() { Type = MessageEntityType.BotCommand } 
            };

            var mockClient = new MockTelegramBotClient()
                .CreateMockSendMessageRequest();

            var mockProvider = new MockServiceProvider();
            mockProvider.AddService(mockClient.Object).Build();

            var commands = new CommandExecutor(mockProvider.GetResult());

            await commands.ExecuteAsync(update);

            Assert.Null(commands.GetUserCommand(chatId));
        }
    }
}
