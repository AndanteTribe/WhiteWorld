using R3;

namespace CardSelectFlow.Interface
{
    public interface ICardStateController
    {
        /// <summary>
        /// カードを表示させたら発火されるイベント
        /// </summary>
        public Observable<Unit> OnEndCardAppear { get; }

        /// <summary>
        /// カードを非表示にさせたら発火されるイベント
        /// </summary>
        public Observable<Unit> OnEndCardDisAppear { get; }
    }
}