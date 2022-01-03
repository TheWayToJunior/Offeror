using Offeror.TelegramBot.Features.Resume;
using Offeror.TelegramBot.Features.Vacancy;

namespace Offeror.TelegramBot.Contracts
{
    public interface IVisitor
    {
        Task VisitAsync(GetResumeResponse resume);

        Task VisitAsync(GetVacancyResponse vacancy);
    }
}
