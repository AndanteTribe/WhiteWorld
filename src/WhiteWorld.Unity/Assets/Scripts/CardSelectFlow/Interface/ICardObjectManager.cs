using R3;

namespace CardSelectFlow.Interface
{
    public interface ICardObjectManager
    {
        public Observable<CardInfo> OnSelected { get; }
        public void Reset();
    }
}