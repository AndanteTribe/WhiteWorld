using System;
using R3;

namespace CardSelectFlow.Interface
{
    public interface ICardSelectFlowController
    {
        /// <summary>
        /// フロウが始まったときに発火されるイベント
        /// </summary>
        public Observable<Unit> OnStartCardSelectFlow { get; }
        /// <summary>
        /// カードが出現するアニメーションが始まったときに発火されるイベント
        /// </summary>
        public Observable<Unit> OnStartAppearCardFlow { get; }
        /// <summary>
        /// カードが出現するアニメーションが終わったときに発火されるイベント
        /// </summary>
        public Observable<Unit> OnEndAppearCardFlow { get; }
        /// <summary>
        /// カードが選択されたときに発火されるイベント
        /// </summary>
        public Observable<Unit> OnCardSelected { get; }
        /// <summary>
        /// カードが消えるアニメーションが始まった時に発火されるイベント
        /// </summary>
        public Observable<Unit> OnStartCardDisappearAnimation { get; }
        /// <summary>
        /// カードが消えるアニメーションが終わったときに発火されるイベント
        /// </summary>
        public Observable<Unit> OnEndCardDisappearAnimation { get; }
        /// <summary>
        /// フロウが終わったときに発火されるイベント
        /// </summary>
        public Observable<Unit> OnEndCardSelectFlow { get; }
    }
}