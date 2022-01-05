using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Offeror.TelegramBot.Commands;
using Offeror.TelegramBot.Commands.Start.States;
using Offeror.TelegramBot.Constants;
using Offeror.TelegramBot.Contracts;
using Offeror.TelegramBot.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Xunit;

namespace Offeror.TelegramBot.Tests
{
    public class StartCommandTest
    {
        private static Mock<IServiceProvider> CreateMockProvider()
        {
            var serviceProvider = new Mock<IServiceProvider>();

            var serviceScope = new Mock<IServiceScope>();
            serviceScope.Setup(x => x.ServiceProvider).Returns(serviceProvider.Object);

            var serviceScopeFactory = new Mock<IServiceScopeFactory>();
            serviceScopeFactory
                .Setup(x => x.CreateScope())
                .Returns(serviceScope.Object);

            serviceProvider
                .Setup(x => x.GetService(typeof(IServiceScopeFactory)))
                .Returns(serviceScopeFactory.Object);

            return serviceProvider;
        }

        private static Mock<ITelegramBotClient> CreateMockSendTextClient()
        {
            var client = new Mock<ITelegramBotClient>();
            client.Setup(x => x.MakeRequestAsync(It.IsAny<SendMessageRequest>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(Mock.Of<Message>()));

            return client;
        }

        private static Update CreateUpdate(long chatId, string text) =>
            new() { Message = new Message { Chat = new Chat { Id = chatId }, Text = text } };

        public static SearchFilter CreateSearchFilter<T>(string prop, T value)
        {
            var builder = new SearchFilter.SearchFilterBuilder();
            builder.SetProperty(prop, value);

            return builder.GetFilter();
        }

        [Fact]
        public async Task DisplayProfilesState_SwitchState()
        {
            var serviceProvider = CreateMockProvider();
            var client = CreateMockSendTextClient();

            serviceProvider
                .Setup(x => x.GetService(typeof(DisplayProfilesState)))
                .Returns(new DisplayProfilesState(client.Object));

            serviceProvider
                .Setup(x => x.GetService(typeof(SetProfileState)))
                .Returns(new SetProfileState(null));

            Update update = CreateUpdate(1, "Test");
            var command = new StartCommand(serviceProvider.Object);

            await command.InvokeAsync(update);

            Assert.IsType<SetProfileState>(command.CommandState);
        }

        [Fact]
        public async Task SetProfileState_ButtonNext_StateLoop()
        {
            var serviceProvider = CreateMockProvider();

            /// Default State for StartCommand
            serviceProvider
                .Setup(x => x.GetService(typeof(DisplayProfilesState)))
                .Returns(new Mock<DisplayProfilesState>(null).Object);

            var response = new Mock<IAnnouncement>();
            response
                .Setup(x => x.AcceptAsync(It.IsAny<IVisitor>()))
                .Verifiable();

            var mediator = new Mock<IMediator>();
            mediator
                .Setup(x => x.Send(It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult((object?)response.Object));

            var reader = new Mock<ISearchFilterReader>();
            reader.Setup(x => x.GetFilter())
                .Returns(CreateSearchFilter(nameof(SearchFilter.Status), Requests.Vacancy));

            serviceProvider
                .Setup(x => x.GetService(typeof(DisplaySearchState)))
                .Returns(new DisplaySearchState(null, reader.Object, mediator.Object));

            var state = new SetSearchState();
            serviceProvider
                .Setup(x => x.GetService(typeof(SetSearchState)))
                .Returns(state);

            Update update = CreateUpdate(1, Buttons.Next);

            var command = new StartCommand(serviceProvider.Object);
            command.UpdateState(state);

            await command.InvokeAsync(update);

            Assert.IsType<SetSearchState>(command.CommandState);
        }

        [Fact]
        public async Task SetProfileState_ButtonRestart_StateRestart()
        {
            var serviceProvider = CreateMockProvider();
            var client = CreateMockSendTextClient();

            /// Default State for StartCommand
            serviceProvider
                .Setup(x => x.GetService(typeof(DisplayProfilesState)))
                .Returns(new DisplayProfilesState(client.Object));

            serviceProvider
                .Setup(x => x.GetService(typeof(SetProfileState)))
                .Returns(new SetProfileState(null));

            var state = new SetSearchState();
            serviceProvider
                .Setup(x => x.GetService(typeof(SetSearchState)))
                .Returns(state);

            Update update = CreateUpdate(1, Buttons.Restart);
            var command = new StartCommand(serviceProvider.Object);
            command.UpdateState(state);

            await command.InvokeAsync(update);

            Assert.IsType<SetProfileState>(command.CommandState);
        }
    }
}
