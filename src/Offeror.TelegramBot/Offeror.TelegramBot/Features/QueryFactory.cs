using Offeror.TelegramBot.Constants;
using Offeror.TelegramBot.Features.Resume;
using Offeror.TelegramBot.Features.Vacancy;
using Offeror.TelegramBot.Models;

namespace Offeror.TelegramBot.Features
{
    public class QueryFactory
    {
        public object CreateRequest(SearchFilter filter) =>
            filter.Status switch
            {
                Requests.Resume => new GetResumeQuery() 
                {
                    Region = filter.Region, 
                    Keywords = new List<string>(filter.Keywords),
                },

                Requests.Vacancy => new GetVacancyQuery() 
                { 
                    Region = filter.Region,
                    Keywords = new List<string>(filter.Keywords),
                },

                _ => throw new ArgumentException("Failed to create request")
            };
    }
}
