using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WhiteWorld.Domain.Entity;

namespace CardSelectFlow
{
    public class CardObject : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _tmPro;
        private Button _btn;

        private void Awake()
        {
            _btn = GetComponentInChildren<Button>();
            if(_btn == null)
                Debug.LogError("Buttonが取得できません");
        }

        private SpaceAmount _spaceAmount;
        private readonly Subject<SpaceAmount> _onSelected = new();

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
        public async UniTask<SpaceAmount> WaitClick(CancellationToken token)
        {
            // UnityEventを変換
            var buttonEvent = _btn.onClick.GetAsyncEventHandler(token);

            // ボタンの入力待ち
            await buttonEvent.OnInvokeAsync();

            return _spaceAmount;
        }
    }
}