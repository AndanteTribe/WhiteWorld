using System;
using System.Threading;
using CardSelectFlow;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using WhiteWorld.AppMain;
using WhiteWorld.Domain.Entity;
using WhiteWorld.Domain.LifeGame.Sequences;

namespace WhiteWorld.Domain.Runtime.Domain.LifeGame.Sequences
{
    /// <summary>
    /// CardSelectionのフロー実行するクラス
    /// </summary>
    public class CardSelectPresenter : MonoBehaviour
    {
        [SerializeField] private CardAnimationManager _animation;
        [SerializeField] private CardObjectManager _manager;

        [Inject]
        public ICardSelectionSequence Sequence;

        private void Start()
        {
            var token = destroyCancellationToken;
            PlayerSelectionFlowAsync(token).Forget();
        }

        /// <summary>
        /// カード表示→Playerの操作を待つ→カード非表示までの流れを実行し、
        /// 選択したSpaceAmountを返す
        /// </summary>
        public async UniTask PlayerSelectionFlowAsync(CancellationToken token)
        {
            //カード情報更新
            _manager.UpdateCard();

            //出現アニメーション
            _animation.Appear().Forget();

            //Playerの選択を待ち、選択した数字を取得
            SpaceAmount spaceAmount = await _manager.WaitPlayerSelectAsync(token);

            //カードを消すアニメーション
            await _animation.DisAppear();

            Sequence.FinishCardSelect(spaceAmount);
        }
    }
}