using MediatR;

namespace Offeror.TelegramBot.Features.Vacancy
{
    public class GetVacancyQueryHandler : IRequestHandler<GetVacancyQuery, GetVacancyResponse>
    {
        public Task<GetVacancyResponse> Handle(GetVacancyQuery request, CancellationToken cancellationToken)
        {
            GetVacancyResponse announcement = new()
            {
                Position = "Middle .NET Developer",
                CompanyName = "Microsoft",
                Salary= "3200 - 5400$",
                KeySkills = new[] { ".NET", "ASP.NET Core", "EF Core" },
                Link = "https://hh.ru/resume/63d915a1ff09634cde0039ed1f654954555936"
            };

            return Task.FromResult(announcement);
        }
    }
}
