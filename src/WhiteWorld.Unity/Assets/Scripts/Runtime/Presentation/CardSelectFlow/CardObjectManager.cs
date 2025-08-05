using System;
using System.Linq;
using System.Threading;
using CardSelectFlow.Interface;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using WhiteWorld.Domain.Entity;

namespace CardSelectFlow
{
    public class CardObjectManager : MonoBehaviour
    {
        //カードオブジェクト
        [SerializeField] private CardObject[] _cardObjs;

        //出すカードを決定するアルゴリズム
        [Inject]
        public IAppearCardDecisionAlgorithm Algorithm;

        public async UniTask<SpaceAmount> WaitPlayerSelectAsync(CancellationToken token)
        {
            var tasks = _cardObjs
                .Select(obj => obj.WaitClick(token))
                .ToArray();

            var (_,spaceAmount) = await UniTask.WhenAny(tasks);

            return spaceAmount;
        }

        /// <summary>
        /// カードを更新する
        /// </summary>
        public void UpdateCard()
        {
            //リセット
            SetSpaceAmount();
        }

        public void UpdateAlgorithm() => throw new NotImplementedException();

        /// <summary>
        /// カード生成アルゴリズムの更新
        /// </summary>
        public void UpdateAlgorithm(IAppearCardDecisionAlgorithm algorithm)
        {
            Algorithm = algorithm;
        }

        /// <summary>
        /// カード情報を登録する関数
        /// </summary>
        private void SetSpaceAmount()
        {
             var infos = Algorithm.GetAppearCards();

             if (infos.Count != _cardObjs.Length)
                 throw new Exception("カード情報とScene上のカードの数がことなっています");

             for (int i = 0; i < _cardObjs.Length; i++)
             {
                 _cardObjs[i].UpdateCardInfo(infos[i]);
             }
        }
    }
}