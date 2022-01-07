namespace Offeror.TelegramBot.Contracts
{
    public interface IStateContainer
    {
        IState GetState<T>() where T : IState;
    }
}
