using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using LitMotion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WhiteWorld.Presentation
{
    /// <summary>
    /// Cardのアニメーションの処理内容が置いてあるクラス
    /// </summary>
    public class CardAnimation : MonoBehaviour
    {
        //ターン時間
        [SerializeField] private float _turnDurationSec = 0.3f;

        //裏面の構成要素
        [SerializeField] private GameObject _backSideElement;

        [SerializeField] private TextMeshProUGUI _tmPro;
        [SerializeField] private Button _btn;
        private Color _defaultTextColor;

        private void Awake()
        {
            _defaultTextColor = _tmPro.color;

            SwitchToBack();

            _btn.transition = Selectable.Transition.None;
            _btn.interactable = false;
        }


        /// <summary>
        /// ターンして表を向ける
        /// </summary>
        public async UniTask TurnToFrontAsync(CancellationToken token)
        {
            await LMotion.Create(1.0f, 0f, _turnDurationSec/2)
                .WithEase(Ease.InSine)
                .Bind(transform, static (x, t) =>
                {
                    var p = t.localScale;
                    p.x = x;
                    t.localScale = p;
                }).ToUniTask(token);

            SwitchToFront();

            await LMotion.Create(0f, 1f, _turnDurationSec/2)
                .WithEase(Ease.OutSine)
                .Bind(transform, static (x, t) =>
                {
                    var p = t.localScale;
                    p.x = x;
                    t.localScale = p;
                }).ToUniTask(token);

            _btn.transition = Selectable.Transition.ColorTint;
            _btn.interactable = true;
        }

        /// <summary>
        /// ターンして裏を向ける
        /// </summary>
        public async UniTask TurnToBackAsync(CancellationToken token)
        {
            _btn.transition = Selectable.Transition.None;
            _btn.interactable = false;

            await LMotion.Create(1.0f, 0f, _turnDurationSec/2)
                .WithEase(Ease.InSine)
                .Bind(transform, static (x, t) =>
                {
                    var p = t.localScale;
                    p.x = x;
                    t.localScale = p;
                }).ToUniTask(token);

            SwitchToBack();

            await LMotion.Create(0f, 1f, _turnDurationSec/2)
                .WithEase(Ease.OutSine)
                .Bind(transform, static (x, t) =>
                {
                    var p = t.localScale;
                    p.x = x;
                    t.localScale = p;
                }).ToUniTask(token);
        }

        /// <summary>
        /// 表示要素を表向きのものにする
        /// </summary>
        private void SwitchToFront()
        {
            _tmPro.color = _defaultTextColor;

            _backSideElement.SetActive(false);
        }

        /// <summary>
        /// 表示要素を裏向きのものにする
        /// </summary>
        private void SwitchToBack()
        {
            _tmPro.color = new Color(0, 0, 0, 0);

            _backSideElement.SetActive(true);
        }
    }
}