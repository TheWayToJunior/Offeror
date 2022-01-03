using Offeror.TelegramBot.Constants;
using Offeror.TelegramBot.Features.Resume;
using Offeror.TelegramBot.Features.Vacancy;

namespace Offeror.TelegramBot.Features
{
    public class QueryFactory
    {
        public object CreateRequest(string? status) =>
            status switch
            {
                Requests.Resume => new GetResumeQuery(),
                Requests.Vacancy => new GetVacancyQuery(),

                _ => throw new ArgumentException("Failed to create request")
            };
    }
}
