using System.Collections.Generic;
using CardSelectFlow.Interface;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CardSelectFlow
{
    /// <summary>
    /// カードのアニメーションを管理するクラス
    /// </summary>
    public class CardAnimationManager : MonoBehaviour,ICardAppearAnimation,ICardDisAppearAnimation
    {
        /// <summary>
        /// 表示アニメーション開始する
        /// </summary>
        public async UniTask Appear(List<int> cardNumbers)
        {
            await UniTask.Delay(100);
        }

        /// <summary>
        /// 非表示アニメーション開始する
        /// </summary>
        public async UniTask DisAppear()
        {
            await UniTask.Delay(100);
        }
    }
}