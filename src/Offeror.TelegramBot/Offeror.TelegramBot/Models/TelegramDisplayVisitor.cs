using Offeror.TelegramBot.Contracts;
using Offeror.TelegramBot.Features.Resume;
using Offeror.TelegramBot.Features.Vacancy;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace Offeror.TelegramBot.Models
{
    public class TelegramDisplayVisitor : IVisitor
    {
        private readonly ITelegramBotClient _client;

        public TelegramDisplayVisitor(ITelegramBotClient client, long chatId, ReplyKeyboardMarkup markup)
        {
            _client = client;

            ChatId = chatId;
            Markup = markup;
        }

        public long ChatId { get; }

        public ReplyKeyboardMarkup Markup { get; set; }

        public async Task VisitAsync(GetResumeResponse resume)
        {
            var builder = new StringBuilder();
            builder.Append(resume.FirstName);

            if (!string.IsNullOrEmpty(resume.LastName))
            {
                builder.Append($" {resume.LastName}");
            }

            builder.Append('\n');
            builder.Append($"{string.Join("\n", resume.Contacts)}\n\n");

            builder.Append($"Position: {resume.Position}\n");
            builder.Append($"Experience: {resume.Experience}\n");
            builder.Append($"Skils: {string.Join(", ", resume.KeySkills)}\n");

            builder.Append('\n');
            builder.Append($"Link: {resume.Link}\n");

            await _client.SendTextMessageAsync(ChatId, builder.ToString(), replyMarkup: Markup);
        }

        public async Task VisitAsync(GetVacancyResponse vacancy)
        {
            var builder = new StringBuilder();
            builder.Append(vacancy.Position);
            builder.Append('\n');

            if (!string.IsNullOrEmpty(vacancy.Salary))
            {
                builder.Append(vacancy.Salary);
                builder.Append('\n');
            }

            builder.Append('\n');
            builder.Append($"Company name: {vacancy.CompanyName}\n");
            builder.Append($"Skils: {string.Join(", ", vacancy.KeySkills)}\n");

            builder.Append('\n');
            builder.Append($"Link: {vacancy.Link}\n");

            await _client.SendTextMessageAsync(ChatId, builder.ToString(), replyMarkup: Markup);
        }
    }
}
