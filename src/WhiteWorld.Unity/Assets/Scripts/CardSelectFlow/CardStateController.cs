using System.Collections.Generic;
using CardSelectFlow.Interface;
using Cysharp.Threading.Tasks;
using R3;

namespace CardSelectFlow
{
    /// <summary>
    /// 表示カードの状態を管理するクラス
    /// 表示、非表示、カード番号など
    /// </summary>
    public class CardStateController : ICardStateController
    {
        //TODO INJECT
        //出すカードを決定するアルゴリズム
        private IAppearCardDecisionAlgorithm _algorithm;
        //カードを表示するアニメーション
        private ICardAppearAnimation _appearAnimation;
        //カードを非表示にするアニメーション
        private ICardDisAppearAnimation _disAppearAnimation;

        public Observable<Unit> OnEndCardAppear => _onEndCardAppear;
        public Observable<Unit> OnEndCardDisAppear => _onEndCardDisAppear;

        private readonly Subject<Unit> _onEndCardAppear = new();
        private readonly Subject<Unit> _onEndCardDisAppear = new();


        private async UniTask AppearFlow()
        {
            //出現するカードを決定
            var cardNumbers = _algorithm.GetAppearCards();

            //出現アニメーション
            await _appearAnimation.Appear(cardNumbers);

            //通知
            _onEndCardAppear.OnNext(Unit.Default);
        }

        private async UniTask DisAppearFlow()
        {
            //出現アニメーション
            await _disAppearAnimation.DisAppear();

            //通知
            _onEndCardDisAppear.OnNext(Unit.Default);
        }
    }
}