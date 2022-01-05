using Offeror.TelegramBot.Contracts;
using Offeror.TelegramBot.Extensions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Offeror.TelegramBot.Commands.Start.States
{
    public class DisplayRegionsState : IState
    {
        private readonly ITelegramBotClient _client;

        public DisplayRegionsState(ITelegramBotClient client)
        {
            _client = client;
        }

        private static ReplyKeyboardMarkup ReplyKeyboardMarkup =>
            new(
                new[]
                {
                    new KeyboardButton[] { Buttons.Russia, Buttons.Ukraine },
                })
            {
                ResizeKeyboard = true
            };

        public async Task ExecuteAsync(IBotCommand command, Update update)
        {
            long chatId = update.GetChatId();

            await _client.SendTextMessageAsync(chatId, "Specify the region to search for",
                replyMarkup: ReplyKeyboardMarkup);

            command.UpdateState(command.Cast<IStateContainer>().GetState<SetRegionState>());
        }
    }
}
