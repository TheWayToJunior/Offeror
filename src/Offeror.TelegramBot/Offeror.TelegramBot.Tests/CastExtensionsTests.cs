using Offeror.TelegramBot.Commands;
using Offeror.TelegramBot.Commands.Start.States;
using Offeror.TelegramBot.Contracts;
using Offeror.TelegramBot.Extensions;
using Offeror.TelegramBot.Tests.Mocks;
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
            var serviceProviderBuilder = new MockServiceProvider();
            serviceProviderBuilder
                .AddService(new ProfileDisplayState(null))
                .Build();

            IBotCommand botCommand = new StartCommand(serviceProviderBuilder.GetResult());

            var container = botCommand.Cast<IStateContainer>();

            Assert.NotNull(container);
            Assert.IsAssignableFrom<IStateContainer>(container);
        }
    }
}