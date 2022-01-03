using Offeror.TelegramBot.Contracts;
using Offeror.TelegramBot.Extensions;
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
            long chatId = update.GetChatId();

            await _client.SendTextMessageAsync(chatId, "Please indicate which status suits you",
                replyMarkup: ReplyKeyboardMarkup);

            command.UpdateState(command.Cast<IStateContainer>().GetState<SetProfileState>());
        }
    }
}
