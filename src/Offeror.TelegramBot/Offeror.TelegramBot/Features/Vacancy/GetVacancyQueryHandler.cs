using MediatR;

namespace Offeror.TelegramBot.Features.Vacancy
{
    public class GetVacancyQueryHandler : IRequestHandler<GetVacancyQuery, GetVacancyResponse>
    {
        public Task<GetVacancyResponse> Handle(GetVacancyQuery request, CancellationToken cancellationToken)
        {
            /// To diversify the development a little
            var random = new Random(DateTime.Now.Millisecond);
            string[] companyName = new[] { "Microsoft", "Google", "Oracle", "Яндекс", "JetBrains" };

            GetVacancyResponse announcement = new()
            {
                Position = "Middle Developer",
                CompanyName = companyName[random.Next(0, 5)],
                Salary = $"{random.Next(5000)}$",
                KeySkills = request.Keywords,
                Description =  
                    $"\nPeople-oriented management without bureaucracy\n" +
                    $"The friendly climate inside the company which is confirmed by the frequent come back of previous\n" +
                    $"employees\n" +
                    $"Flexible working schedule\n" +
                    $"Paid time off (20 working days per year, plus all national holidays and 9 sick days)\n\n" +
                    $"Laptop of your choice: MacBook Pro or Windows/Linux business laptop + large extra screen\n" +
                    $"Full financial and legal support for private entrepreneurs\n" +
                    $"Education compensation\n" +
                    $"Free English classes with native speaker\n",
                Link = "https://hh.ru/resume/63d915a1ff09634cde0039ed1f654954555936"
            };

            return Task.FromResult(announcement);
        }
    }
}
