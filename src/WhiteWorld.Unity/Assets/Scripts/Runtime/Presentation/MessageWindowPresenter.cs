using System.Threading;
using AndanteTribe.Utils;
using Cysharp.Threading.Tasks;
using LitMotion;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using VContainer;
using WhiteWorld.Domain.Entity;

namespace WhiteWorld.Presentation
{
    public class MessageWindowPresenter : MonoBehaviour
    {
        [SerializeField]
        private GameObject _window;
        [SerializeField]
        private TextMeshProUGUI _nameText;
        [SerializeField]
        private TextMeshProUGUI _messageText;
        [SerializeField]
        private Button _screenBtn;
        [SerializeField]
        private Graphic _fadeout;

        private MessagePlayData _data;
        private AutoResetUniTaskCompletionSource _onFinish;

        [Inject]
        public void Initialize(MessagePlayData data, AutoResetUniTaskCompletionSource onFinish)
        {
            _data = data;
            _onFinish = onFinish;
        }

        private async UniTaskVoid Start()
        {
            if (_data.IsFadeAnim)
            {
                _window.SetActive(false);
                await LMotion.Create(0f, 1.0f, 2)
                    .Bind(_fadeout, static (v, fadeout) =>
                    {
                        var color = fadeout.color;
                        color.a = v;
                        fadeout.color = color;
                    })
                    .ToUniTask(destroyCancellationToken);
                await UniTask.WaitForSeconds(1, cancellationToken: destroyCancellationToken);
                _window.SetActive(true);
            }

            foreach (var model in _data.Messages)
            {
                _nameText.text = model.Name;
                _messageText.text = model.Message;

                await WaitNextAsync(destroyCancellationToken);
            }
            _onFinish.TrySetResult();
        }

        private UniTask WaitNextAsync(CancellationToken cancellationToken) =>
            UniTask.WhenAny(
                _screenBtn.OnClickAsync(cancellationToken),
                UniTask.WaitUntil(static () => Keyboard.current is { spaceKey: { wasPressedThisFrame: true } }, cancellationToken: cancellationToken)
            );
    }
}