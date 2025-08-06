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
        private Button _btn;
        private TextMeshProUGUI _tmPro;
        private SpaceAmount _spaceAmount;

        private void Awake()
        {
            _btn = GetComponentInChildren<Button>();
            if(_btn == null)
                Debug.LogError("Buttonが取得できません");

            _tmPro = GetComponentInChildren<TextMeshProUGUI>();
            if(_tmPro == null)
                Debug.LogError("TextMeshが取得できません");
        }

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
            _btn.enabled = false;

            //自身の情報をCardInfoに代入
            CardInfo cardInfo;
            cardInfo.Amount = _spaceAmount;
            cardInfo.PositionType = gameObject.GetComponent<CardSlotHolder>().CardSlot;

            return cardInfo;
        }
    }
}