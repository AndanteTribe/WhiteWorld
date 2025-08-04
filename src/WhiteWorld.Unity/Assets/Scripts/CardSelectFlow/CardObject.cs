using System;
using R3;
using TMPro;
using UnityEngine;

namespace CardSelectFlow
{
    public class CardObject : MonoBehaviour
    {
        /// <summary>
        /// カードボタンがクリックされたときに発火されるイベント
        /// カード情報を持つ
        /// </summary>
        public Observable<CardInfo> OnClicked => _onClicked;

        [SerializeField] private TextMeshProUGUI _tmPro;

        private CardInfo _cardInfo;
        private readonly Subject<CardInfo> _onClicked = new();

        /// <summary>
        /// カード情報を更新する
        /// </summary>
        public void SetCardInfo(CardInfo cardInfo)
        {
            _cardInfo = cardInfo;
            _tmPro.text = _cardInfo.Number.ToString();
        }

        /// <summary>
        /// ButtonEventから呼び出される関数
        /// </summary>
        public void Clicked()
        {
            _onClicked.OnNext(_cardInfo);
        }
    }
}