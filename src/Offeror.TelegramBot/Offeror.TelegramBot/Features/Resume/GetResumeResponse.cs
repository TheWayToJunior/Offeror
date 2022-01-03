using Offeror.TelegramBot.Contracts;

namespace Offeror.TelegramBot.Features.Resume
{
    /// TODO: Complement the rest of the set of properties
    public class GetResumeResponse : IAnnouncement
    {
        public string FirstName { get; set; }

        public string? LastName { get; set; }

        public IEnumerable<string> Contacts { get; set; }

        public string Position { get; set; }

        public string Experience { get; set; }

        public IEnumerable<string> KeySkills { get; set; }

        public string Link { get; set; }

        public async Task AcceptAsync(IVisitor visitor)
        {
            await visitor.VisitAsync(this);
        }
    }
}
