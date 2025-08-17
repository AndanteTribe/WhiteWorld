using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace WhiteWorld.Presentation
{
    /// <summary>
    /// カードのアニメーションを管理するクラス
    /// </summary>
    public class CardAnimationManager : MonoBehaviour
    {
        /// <summary>
        /// 表示アニメーション全体の遅延時間（秒）
        /// </summary>
        [SerializeField] private float _appearDelaySec = 1f;
        /// <summary>
        /// カードを表示するアニメーションの始点間のインターバル時間（秒）
        /// </summary>
        [SerializeField] private float _turnStartIntervalSec = 0.5f;
        /// <summary>
        /// 非表示アニメーション全体の遅延時間（秒）
        /// </summary>
        [SerializeField] private float _disappearDelaySec = 1f;

        [SerializeField] private CardAnimation[] _animations;

        [Inject] public AudioController _audioController;

        /// <summary>
        /// 表示アニメーション開始する
        /// </summary>
        public async UniTask AppearAsync(CancellationToken token)
        {
            UniTask currentTask = UniTask.CompletedTask;

            await UniTask.Delay(TimeSpan.FromSeconds(_appearDelaySec), cancellationToken: token);

            foreach (var anim in _animations)
            {
                currentTask = anim.TurnToFrontAsync(token);

                //カードをめくる効果音を鳴らす
                _audioController.PlaySE(1, token).Forget();

                await UniTask.Delay(TimeSpan.FromSeconds(_turnStartIntervalSec),cancellationToken:token);
            }

            //最後のタスクを待つ
            var lastTask = currentTask;
            await lastTask;
        }

        /// <summary>
        /// 非表示アニメーション開始する
        /// </summary>
        public async UniTask DisAppearAsync(CardSlot selectedSlot,CancellationToken token)
        {
            UniTask currentTask = UniTask.CompletedTask;

            await UniTask.Delay(TimeSpan.FromSeconds(_disappearDelaySec),cancellationToken:token);

            foreach (var anim in _animations)
            {
                //対象のSlotを取得し、選択されたカードなら裏返さない
                var targetSlot = anim.gameObject.GetComponent<CardSlotHolder>().CardSlot;
                if(targetSlot == selectedSlot)
                    continue;

                //裏返すのは同時に
                currentTask = anim.TurnToBackAsync(token);
            }

            //最後のタスクを待つ
            var lastTask = currentTask;
            await lastTask;
        }
    }
}