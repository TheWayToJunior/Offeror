using MediatR;
using Moq;
using Offeror.TelegramBot.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace Offeror.TelegramBot.Tests.Mocks
{
    internal class MockMediatorFactory
    {
        public Mock<IMediator> CreateAnnouncementQueryHandler()
        {
            var response = new Mock<IAnnouncement>();
            response
                .Setup(x => x.AcceptAsync(It.IsAny<IVisitor>()))
                .Verifiable();

            var mediator = new Mock<IMediator>();
            mediator
                .Setup(x => x.Send(It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult((object?)response.Object));

            return mediator;
        }
    }
}
