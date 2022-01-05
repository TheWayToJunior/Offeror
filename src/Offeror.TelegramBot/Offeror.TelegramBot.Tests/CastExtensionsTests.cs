using Microsoft.Extensions.DependencyInjection;
using Moq;
using Offeror.TelegramBot.Commands;
using Offeror.TelegramBot.Commands.Start.States;
using Offeror.TelegramBot.Contracts;
using Offeror.TelegramBot.Extensions;
using System;
using Xunit;

namespace Offeror.TelegramBot.Tests
{
    public class CastExtensionsTests
    {
        [Fact]
        public void ObjectCastToString_ExceptionIsNull()
        {
            object @string = "test";

            string str = @string.Cast<string>();

            Assert.False(string.IsNullOrWhiteSpace(str));
            Assert.Equal(str, @string);
        }

        [Fact]
        public void NumberCastToString_ExceptionIsNotNull()
        {
            object number = 1;

            var exception = Assert.Throws<InvalidCastException>(() =>
            {
                string str = number.Cast<string>();
            });

            Assert.NotNull(exception);
            Assert.IsType<InvalidCastException>(exception);
        }

        [Fact]
        public void BotCommandCast_ExceptionIsNotNull()
        {
            var serviceProvider = new Mock<IServiceProvider>();

            var serviceScope = new Mock<IServiceScope>();
            serviceScope.Setup(x => x.ServiceProvider).Returns(serviceProvider.Object);

            serviceProvider
                .Setup(x => x.GetService(typeof(DisplayProfilesState)))
                .Returns(new DisplayProfilesState(null));

            var serviceScopeFactory = new Mock<IServiceScopeFactory>();
            serviceScopeFactory
                .Setup(x => x.CreateScope())
                .Returns(serviceScope.Object);

            serviceProvider
                .Setup(x => x.GetService(typeof(IServiceScopeFactory)))
                .Returns(serviceScopeFactory.Object);

            IBotCommand botCommand = new StartCommand(serviceProvider.Object);

            var container = botCommand.Cast<IStateContainer>();

            Assert.NotNull(container);
            Assert.IsAssignableFrom<IStateContainer>(container);
        }
    }
}