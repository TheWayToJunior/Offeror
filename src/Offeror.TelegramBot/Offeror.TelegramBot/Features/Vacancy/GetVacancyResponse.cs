using Offeror.TelegramBot.Contracts;

namespace Offeror.TelegramBot.Features.Vacancy
{
    /// TODO: Complement the rest of the set of properties
    public class GetVacancyResponse : IAnnouncement
    {
        public string CompanyName { get; set; }

        public string Position { get; set; }

        public string? Salary { get; set; }

        public IEnumerable<string> KeySkills { get; set; }

        public string Link { get; set; }

        public async Task AcceptAsync(IDisplayVisitor visitor)
        {
            await visitor.VisitAsync(this);
        }
    }
}
