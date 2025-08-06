using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace WhiteWorld.Presentation
{
    /// <summary>
    /// カードのアニメーションを管理するクラス
    /// </summary>
    public class CardAnimationManager : MonoBehaviour
    {
        [SerializeField] private float _appearDelaySec = 1f;
        [SerializeField] private float _turnIntervalSec = 0.1f;
        [SerializeField] private float _disappearDelaySec = 1f;
        [SerializeField] private CardAnimation[] _animations;

        /// <summary>
        /// 表示アニメーション開始する
        /// </summary>
        public async UniTask AppearAsync()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_appearDelaySec));

            foreach (var anim in _animations)
            {
                anim.TurnToFrontAsync().Forget();
                await UniTask.Delay(TimeSpan.FromSeconds(_turnIntervalSec));
            }
        }

        /// <summary>
        /// 非表示アニメーション開始する
        /// </summary>
        public async UniTask DisAppearAsync(CardSlot selectedSlot)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_disappearDelaySec));

            foreach (var anim in _animations)
            {
                //対象のSlotを取得し、選択されたカードなら裏返さない
                var targetSlot = anim.gameObject.GetComponent<CardSlotHolder>().CardSlot;
                if(targetSlot == selectedSlot)
                    continue;

                //裏返すのは同時に
                anim.TurnToBackAsync().Forget();
            }
        }
    }
}