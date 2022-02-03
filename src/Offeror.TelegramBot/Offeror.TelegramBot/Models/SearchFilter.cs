using Offeror.TelegramBot.Contracts;

namespace Offeror.TelegramBot.Models
{
    public sealed class SearchFilter
    {
        private SearchFilter()
        {
        }

        public string Status { get; set; }

        public string Region { get; set; }

        public ICollection<string> Keywords { get; set; }

        /// The class is nested to be able to create Search Filter instances with a private constructor
        public class Builder : ISearchFilterBuilder
        {
            private readonly SearchFilter _filter;

            public Builder()
            {
                _filter = new SearchFilter() 
                {
                    Keywords = new List<string>()
                };
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

            public ISearchFilterWriter AppendKeyword(string keyword)
            {
                if(_filter.Keywords.Contains(keyword))
                {
                    _filter.Keywords.Remove(keyword);
                    return this;
                }

                _filter.Keywords.Add(keyword);
                return this;
            }

            public SearchFilter GetFilter()
            {
                return _filter;
            }

            public ISearchFilterWriter ClearKeyword()
            {
                _filter.Keywords.Clear();
                return this;
            }
        }
    }
}
