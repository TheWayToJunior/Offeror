using Offeror.TelegramBot.Features.Resume;
using Offeror.TelegramBot.Features.Vacancy;

namespace Offeror.TelegramBot.Contracts
{
    public interface IAnnouncement
    {
        Task AcceptAsync(IDisplayVisitor visitor);
    }

    public interface IDisplayVisitor
    {
        Task VisitAsync(GetResumeResponse resume);

        Task VisitAsync(GetVacancyResponse vacancy);
    }
}
