using System;
using System.Linq;
using CardSelectFlow.Interface;
using R3;
using UnityEngine;
using VContainer;

namespace CardSelectFlow
{
    public class CardObjectManager : MonoBehaviour,ICardObjectManager
    {
        //カードオブジェクト
        [SerializeField] private CardObject[] _cardObjs;

        /// <summary>
        /// カードが選択されたときに発火されるイベント
        /// カード情報を持つ
        /// </summary>
        public Observable<CardInfo> OnSelected => _onSelected;
        private readonly Subject<CardInfo> _onSelected = new();

        //出すカードを決定するアルゴリズム
        [Inject]
        private IAppearCardDecisionAlgorithm _algorithm;

        //今回のフローで購読済みかどうか
        private bool _hasSubscribed;

        private void Awake()
        {
            //全てのカードオブジェクトのイベントを監視し、最初の通知だけを購読
            var merged = _cardObjs
                .Select(card => card.OnClicked)
                .Merge()
                .Where(_ => !_hasSubscribed)
                .Subscribe(info =>
                {
                    //通知
                    _onSelected.OnNext(info);
                    //購読した
                    _hasSubscribed = true;
                }).AddTo(this);

        }

        public void Reset()
        {
            //リセット
            _hasSubscribed = false;
            SetCardInfo();
        }

        /// <summary>
        /// カード情報を登録する関数
        /// </summary>
        private void SetCardInfo()
        {
             var infos = _algorithm.GetAppearCards();

             if (infos.Count != _cardObjs.Length)
                 throw new Exception("カード情報とScene上のカードの数がことなっています");

             for (int i = 0; i < _cardObjs.Length; i++)
             {
                 _cardObjs[i].SetCardInfo(infos[i]);
             }
        }
    }
}