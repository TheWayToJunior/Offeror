﻿using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Offeror.TelegramBot.Commands
{
    public class ExecutorExceprionHandler : ICommandExecutor
    {
        private readonly TelegramBotClient _client;

        public ExecutorExceprionHandler(ICommandExecutor commandExecutor, TelegramBotClient client)
        {
            _client = client;
            CommandExecutor = commandExecutor;
        }

        public ICommandExecutor CommandExecutor { get; }

        public async Task ExecuteAsync(Update update)
        {
            try
            {
                await CommandExecutor.ExecuteAsync(update);
            }
            catch (Exception ex)
            {
                long? chatId = update?.Message?.Chat.Id;

                if (chatId is null)
                {
                    throw new ArgumentNullException(nameof(chatId), ex);
                }

                await _client.SendTextMessageAsync(chatId, ex.Message);
            }
        }
    }
}