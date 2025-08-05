using System;
using System.Threading;
using CardSelectFlow.Interface;
using Cysharp.Threading.Tasks;
using WhiteWorld.Domain.Entity;

namespace WhiteWorld.Domain.LifeGame.Sequences
{
    /// <summary>
    /// カード選択のシーケンス.
    /// </summary>
    public class CardSelectionSequence : ILifeGameSequence
    {
        /// <inheritdoc/>
        public LifeGameMode Mode => LifeGameMode.CardSelection;

        private ICardAnimation _animation;
        private ICardObjectManager _manager;

        public CardSelectionSequence(ICardAnimation animation, ICardObjectManager manager)
        {
            Console.WriteLine("コンストラクタ");
            _animation = animation;
            _manager = manager;
        }

        public async UniTask<SpaceAmount> RunAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Run");
            //カード情報更新
            _manager.UpdateCard();

            //出現アニメーション
            _animation.Appear().Forget();

            //Playerの選択を待ち、選択した数字を取得
            SpaceAmount spaceAmount = await _manager.WaitPlayerSelectAsync(cancellationToken);

            //カードを消すアニメーション
            await _animation.DisAppear();

            return spaceAmount;
        }
    }
}