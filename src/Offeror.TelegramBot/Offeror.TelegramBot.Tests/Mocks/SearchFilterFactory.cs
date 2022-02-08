using Offeror.TelegramBot.Models;

namespace Offeror.TelegramBot.Tests.Mocks
{
    internal class SearchFilterFactory
    {
        public static SearchFilter CreateSearchFilter<T>(string prop, T value)
        {
            var builder = new SearchFilter.Builder();
            builder.SetProperty(prop, value);

            return builder.GetFilter();
        }
    }
}
