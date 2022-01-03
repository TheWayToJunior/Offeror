using MediatR;

namespace Offeror.TelegramBot.Features.Vacancy
{
    public class GetVacancyQuery : IRequest<GetVacancyResponse>
    {
        public string Region { get; set; }
    }
}
