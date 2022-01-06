using Moq;
using Offeror.TelegramBot.Commands;
using Offeror.TelegramBot.Commands.Start.States;
using Offeror.TelegramBot.Constants;
using Offeror.TelegramBot.Contracts;
using Offeror.TelegramBot.Models;
using Offeror.TelegramBot.Tests.Mocks;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Xunit;

namespace Offeror.TelegramBot.Tests
{
    public class StartCommandTests
    {
        [Fact]
        public async Task DisplayProfilesState_SwitchState()
        {
            var client = new MockTelegramBotClient().CreateMockSendMessageRequest();

            var serviceProviderBuilder = new MockServiceProvider();
            serviceProviderBuilder
                .AddService(new DisplayProfilesState(client.Object))
                .AddService(new SetProfileState(null))
                .Builde();

            var command = new StartCommand(serviceProviderBuilder.GetResult());
            Update update = UpdateFactory.CreateUpdate(1, "Test");

            /// Act
            await command.InvokeAsync(update);

            Assert.NotNull(command.CommandState);
            Assert.IsType<SetProfileState>(command.CommandState);
        }

        [Fact]
        public async Task SetProfileState_ButtonNext_StateLoop()
        {
            var mediator = MockMediator.CreateAnnouncementQueryHandler();
            var searchFilter = SearchFilterFactory.CreateSearchFilter(nameof(SearchFilter.Status), Requests.Vacancy);
            var currentState = new SetSearchState();

            var reader = new Mock<ISearchFilterReader>();
            reader.Setup(x => x.GetFilter())
                .Returns(searchFilter);

            var serviceProviderBuilder = new MockServiceProvider();
            serviceProviderBuilder
                .AddService(new DisplayProfilesState(null))
                .AddService(new DisplaySearchState(null, reader.Object, mediator.Object))
                .AddService(currentState)
                .Builde();

            var command = new StartCommand(serviceProviderBuilder.GetResult());
            command.UpdateState(currentState);

            Update update = UpdateFactory.CreateUpdate(1, Buttons.Search);

            /// Act
            await command.InvokeAsync(update);

            Assert.NotNull(command.CommandState);
            Assert.IsType<SetSearchState>(command.CommandState);
            Assert.Equal(currentState, command.CommandState);
        }

        [Fact]
        public async Task SetProfileState_ButtonRestart_StateRestart()
        {
            var client = new MockTelegramBotClient().CreateMockSendMessageRequest();
            var currentState = new SetSearchState();

            var serviceProviderBuilder = new MockServiceProvider();
            serviceProviderBuilder
                .AddService(new DisplayProfilesState(client.Object))
                .AddService(new SetProfileState(null))
                .AddService(currentState)
                .Builde();

            var command = new StartCommand(serviceProviderBuilder.GetResult());
            command.UpdateState(currentState);

            Update update = UpdateFactory.CreateUpdate(1, Buttons.Restart);

            /// Act
            await command.InvokeAsync(update);

            Assert.NotNull(command.CommandState);
            Assert.IsType<SetProfileState>(command.CommandState);
        }
    }
}
