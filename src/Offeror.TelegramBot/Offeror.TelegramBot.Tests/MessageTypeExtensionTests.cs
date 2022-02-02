using Offeror.TelegramBot.Extensions;
using System;
using System.Collections.Generic;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Offeror.TelegramBot.Tests
{
    public class MessageTypeExtensionTests
    {
        private static Update CreateUpdate(params MessageEntityType[] types)
        {
            var messageEntitys = new List<MessageEntity>();
            foreach (var type in types)
            {
                messageEntitys.Add(new MessageEntity { Type = type });
            }

            return new Update
            {
                Message = new Message 
                {
                    Entities = messageEntitys.ToArray()
                }
            };
        }

        [Fact]
        public void IsBotCommandMessage_BotCommand()
        {
            var update = CreateUpdate(MessageEntityType.BotCommand);

            bool isCommand = update.IsTypeMessage(MessageEntityType.BotCommand);

            Assert.True(isCommand);
        }

        [Fact]
        public void IsBotCommandMessage_PhoneNumber()
        {
            var update = CreateUpdate(MessageEntityType.PhoneNumber);

            bool isCommand = update.IsTypeMessage(MessageEntityType.BotCommand);

            Assert.False(isCommand);
        }

        [Fact]
        public void IsBotCommandMessage_BotCommandAndPhoneNumber()
        {
            var update = CreateUpdate(MessageEntityType.BotCommand, MessageEntityType.PhoneNumber);

            bool isCommand = update.IsTypeMessage(MessageEntityType.BotCommand);

            Assert.True(isCommand);
        }

        [Fact]
        public void IsBotCommandMessage_DoubleBotCommand()
        {
            var update = CreateUpdate(MessageEntityType.BotCommand, MessageEntityType.BotCommand);

            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                bool isCommand = update.IsTypeMessage(MessageEntityType.BotCommand);
            });
        }
    }
}
