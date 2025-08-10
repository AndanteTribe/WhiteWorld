using System;
using System.Threading;
using CardSelectFlow.Interface;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using ZLinq;

namespace WhiteWorld.Presentation
{
    /// <summary>
    /// 複数のCardObjectを管理するクラス
    /// </summary>
    public class CardObjectManager : MonoBehaviour
    {
        //カードオブジェクト
        [SerializeField] private CardClickWaiter[] _waiter;

        //出すカードを決定するアルゴリズム
        [Inject]
        public IAppearCardDecisionAlgorithm Algorithm;

        /// <summary>
        /// プレイヤーの選択を待つ関数
        /// </summary>
        public async UniTask<CardInfo> WaitPlayerSelectAsync(CancellationToken token)
        {
            using var tasks = _waiter
                .AsValueEnumerable()
                .Select(obj => obj.WaitClick(token))
                .ToArrayPool();

            var (_,info) = await UniTask.WhenAny(tasks.Array);

            return info;
        }

        /// <summary>
        /// カード生成アルゴリズムの更新
        /// </summary>
        public void UpdateAlgorithm(IAppearCardDecisionAlgorithm algorithm)
        {
            Algorithm = algorithm;
        }

        /// <summary>
        /// カード情報を更新する関数
        /// </summary>
        public void UpdateCard()
        {
             var infos = Algorithm.GetAppearCards();

             if (infos.Count != _waiter.Length)
                 throw new Exception("カード情報とScene上のカードの数がことなっています");

             for (int i = 0; i < _waiter.Length; i++)
             {
                 _waiter[i].UpdateCardInfo(infos[i]);
             }
        }
    }
}