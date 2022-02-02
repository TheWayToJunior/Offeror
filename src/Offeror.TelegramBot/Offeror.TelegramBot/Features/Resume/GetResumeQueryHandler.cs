using MediatR;

namespace Offeror.TelegramBot.Features.Resume
{
    public class GetResumeQueryHandler : IRequestHandler<GetResumeQuery, GetResumeResponse>
    {
        public Task<GetResumeResponse> Handle(GetResumeQuery request, CancellationToken cancellationToken)
        {
            GetResumeResponse announcement = new()
            {
                FirstName = "Miha",
                LastName = "Smolenskiy",
                Position = ".NET Developer",
                Experience = "1 year",
                KeySkills = new[] { ".NET", "ASP.NET Core", "EF Core" },
                Contacts = new[] { "+38095914578", "https://github.com/TheWayToJunior" },
                Link = "https://hh.ru/resume/63d915a1ff09634cde0039ed1f654954555936"
            };

            return Task.FromResult(announcement);
        }
    }
}
