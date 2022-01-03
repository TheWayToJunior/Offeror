using Offeror.TelegramBot.Contracts;

namespace Offeror.TelegramBot.Models
{
    public sealed class SearchFilter
    {
        private SearchFilter()
        {
        }

        public string? Status { get; set; }

        public string? Region { get; set; }

        /// The class is nested to be able to create Search Filter instances with a private constructor
        public class SearchFilterBuilder : ISearchFilterBuilder
        {
            private readonly SearchFilter _filter;

            public SearchFilterBuilder()
            {
                _filter = new SearchFilter();
            }

            public ISearchFilterWriter SetProperty<T>(string name, T value) 
            {
                var property = _filter.GetType().GetProperty(name);

                if (property == null)
                {
                    throw new InvalidOperationException();
                }

                property.SetValue(_filter, value);
                return this;
            }

            public SearchFilter GetFilter()
            {
                return _filter;
            }
        }
    }
}
