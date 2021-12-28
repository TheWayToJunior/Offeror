using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Offeror.TelegramBot.Commands.Start.States
{
    public class DisplayRegionsState : IState
    {
        private readonly TelegramBotClient _client;

        public DisplayRegionsState(TelegramBotClient client)
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
            long? chatId = update?.Message?.Chat.Id;

            if (chatId is null)
            {
                throw new ArgumentNullException(nameof(chatId));
            }

            await _client.SendTextMessageAsync(chatId, "Specify your status",
                replyMarkup: ReplyKeyboardMarkup);

            command.SetState(chatId.Value, ((IStateContainer)command).GetState<SetRegionState>());
        }
    }
}
