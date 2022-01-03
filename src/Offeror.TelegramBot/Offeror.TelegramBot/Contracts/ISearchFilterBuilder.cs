using Offeror.TelegramBot.Models;

namespace Offeror.TelegramBot.Contracts
{
    /// Implementation of the separation of interfaces into read and write,
    /// since in display commands it was only possible to read the filter and vice versa
    public interface ISearchFilterBuilder : ISearchFilterWriter, ISearchFilterReader
    {
    }

    public interface ISearchFilterWriter
    {
        ISearchFilterWriter SetProperty<T>(string name, T value);
    }

    public interface ISearchFilterReader
    {
        SearchFilter GetFilter();
    }
}
