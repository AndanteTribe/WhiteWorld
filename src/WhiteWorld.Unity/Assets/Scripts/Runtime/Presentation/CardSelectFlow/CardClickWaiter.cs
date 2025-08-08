using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WhiteWorld.Domain.Entity;

namespace WhiteWorld.Presentation
{
    /// <summary>
    /// カードオブジェクトにアタッチするクラス
    /// ボタンの入力を受け付ける
    /// </summary>
    public class CardClickWaiter : MonoBehaviour
    {
        [SerializeField] private Button _btn;
        [SerializeField] private TextMeshProUGUI _tmPro;
        private SpaceAmount _spaceAmount;

        /// <summary>
        /// カード情報を更新する
        /// </summary>
        public void UpdateCardInfo(SpaceAmount spaceAmount)
        {
            _spaceAmount = spaceAmount;
            _tmPro.text = _spaceAmount.ToString();
        }
        /// <summary>
        /// ボタンのクリックを待つ
        /// </summary>
        public async UniTask<CardInfo> WaitClick(CancellationToken token)
        {
            // UnityEventを変換
            var buttonEvent = _btn.onClick.GetAsyncEventHandler(token);

            // ボタンの入力待ち
            await buttonEvent.OnInvokeAsync();

            //ボタンを押せなくする
            SetButtonInteractableFalse();

            //自身の情報をCardInfoに代入
            CardInfo cardInfo;
            cardInfo.Amount = _spaceAmount;
            cardInfo.PositionType = gameObject.GetComponent<CardSlotHolder>().CardSlot;

            return cardInfo;
        }

        private void SetButtonInteractableFalse()
        {
            //interactable = falseにした後の色を設定
            var colors = _btn.colors;
            colors.disabledColor = colors.normalColor;
            _btn.colors = colors;

            _btn.interactable = false;
        }
    }
}