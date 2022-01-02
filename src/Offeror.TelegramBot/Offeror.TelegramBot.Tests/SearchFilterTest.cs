using Offeror.TelegramBot.Models;
using Xunit;

namespace Offeror.TelegramBot.Tests
{
    public class SearchFilterTest
    {
        [Fact]
        public void SearchFilterBuilder_CreateSearchFilter()
        {
            SearchFilter.SearchFilterBuilder builder = new();

            builder.SetProperty(nameof(SearchFilter.Region), "rus");
            builder.SetProperty(nameof(SearchFilter.Status), "company");
            builder.SetProperty(nameof(SearchFilter.Region), "ukr");

            var filter = builder.GetFilter();

            Assert.NotNull(filter);
            Assert.IsType<SearchFilter>(filter);
            Assert.Equal("ukr", filter.Region);
            Assert.Equal("company", filter.Status);
        }
    }
}
