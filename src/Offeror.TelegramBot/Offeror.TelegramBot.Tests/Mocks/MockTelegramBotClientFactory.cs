using Moq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;

namespace Offeror.TelegramBot.Tests.Mocks
{
    internal class MockTelegramBotClientFactory
    {
        private readonly Mock<ITelegramBotClient> _client;

        public MockTelegramBotClientFactory()
        {
            _client = new Mock<ITelegramBotClient>();
        }

        public Mock<ITelegramBotClient> CreateMockSendMessageRequest()
        {
            _client.Setup(x => x.MakeRequestAsync(It.IsAny<SendMessageRequest>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(Mock.Of<Message>()));

            return _client;
        }
    }
}
