using Offeror.TelegramBot.Contracts;
using Offeror.TelegramBot.Extensions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Offeror.TelegramBot.Commands.Start.States
{
    public class RegionDisplayState : IState
    {
        private readonly ITelegramBotClient _client;

        public RegionDisplayState(ITelegramBotClient client)
        {
            _client = client;
        }

        private static ReplyKeyboardMarkup ReplyKeyboardMarkup =>
            new(
                new[]
                {
                    new KeyboardButton[] { Buttons.Russia, Buttons.Ukraine, Buttons.Back }
                })
            {
                ResizeKeyboard = true
            };

        public async Task ExecuteAsync(IBotCommand command, Update update)
        {
            long chatId = update.GetChatId();

            await _client.SendTextMessageAsync(chatId, "Specify the region to search for",
                replyMarkup: ReplyKeyboardMarkup);
            
            command.UpdateState(command.Cast<IStateContainer>().GetState<RegionInputState>());
        }
    }
}
