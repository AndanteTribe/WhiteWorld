using CardSelectFlow.Interface;
using Cysharp.Threading.Tasks;
using LitMotion;
using UnityEngine;

namespace CardSelectFlow
{
    /// <summary>
    /// カードのアニメーションを管理するクラス
    /// </summary>
    public class CardAnimationManager : MonoBehaviour,ICardAnimation
    {
        [SerializeField] private float _turnDuration;
        [SerializeField] private GameObject[] _cardObjs;

        /// <summary>
        /// 表示アニメーション開始する
        /// </summary>
        public async UniTask Appear()
        {
            //一旦SetActive
            foreach (var obj in _cardObjs)
            {
                obj.SetActive(true);
            }
            await UniTask.Delay(100);
        }

        /// <summary>
        /// 非表示アニメーション開始する
        /// </summary>
        public async UniTask DisAppear()
        {
            //一旦SetActive
            foreach (var obj in _cardObjs)
            {
                obj.SetActive(false);
            }
            await UniTask.Delay(100);
        }

        private void Turn()
        {

        }
    }
}