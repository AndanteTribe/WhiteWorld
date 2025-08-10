using System.Threading;
using Cysharp.Threading.Tasks;
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
        private RectMask2D _fadeinMask;
        [SerializeField]
        private TextMeshProUGUI _nameText;
        [SerializeField]
        private TextMeshProUGUI _messageText;
        [SerializeField]
        private Button _screenBtn;

        private MessageModel[] _messages;
        private AutoResetUniTaskCompletionSource _onFinish;

        [Inject]
        public void Initialize(MessageModel[] messages, AutoResetUniTaskCompletionSource onFinish)
        {
            _messages = messages;
            _onFinish = onFinish;
        }

        private async UniTaskVoid Start()
        {
            foreach (var model in _messages)
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