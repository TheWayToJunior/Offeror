using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Offeror.TelegramBot.Commands.Start.States
{
    public class DisplayProfilesState : IState
    {
        private readonly TelegramBotClient _client;

        public DisplayProfilesState(TelegramBotClient client)
        {
            _client = client;
        }

        private static ReplyKeyboardMarkup ReplyKeyboardMarkup =>
            new(
                new[]
                {
                    new KeyboardButton[] { Buttons.Applicant, Buttons.Сompany },
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

            await _client.SendTextMessageAsync(chatId, "Please indicate which status suits you",
                replyMarkup: ReplyKeyboardMarkup);

            command.UpdateState(chatId.Value, ((IStateContainer)command).GetState<SetProfileState>());
        }
    }
}
