using AndanteTribe.IO;
using Cysharp.Threading.Tasks;
using LitMotion;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using VContainer;
using WhiteWorld.Domain;
using WhiteWorld.Domain.Entity;

namespace WhiteWorld.Presentation
{
    public class OpeningSceneLoader : MonoBehaviour
    {
        [SerializeField]
        private Graphic _fadeIn;
        [SerializeField]
        private TextMeshProUGUI _linkText;

        [Inject]
        private ISceneController _controller;
        private WhiteWorldActions _whiteWorldActions;

        private async UniTaskVoid Start()
        {
            if (LocalPrefs.Shared.Load<bool>(GameConst.ClearFlag))
            {
                _linkText.enabled = true;
                _linkText.GetComponent<Button>()
                    .OnClickAsObservable()
                    .Subscribe(_linkText, static (_, text) =>
                    {
                        var pointer = Pointer.current;
                        if (pointer != null)
                        {
                            var currentPos = pointer.position.ReadValue();
                            var index = TMP_TextUtilities.FindIntersectingLink(text, currentPos, null);
                            if (index != -1)
                            {
                                var info = text.textInfo.linkInfo[index];
                                Application.OpenURL(info.GetLinkID());
                            }
                        }
                    });
            }
            else
            {
                _linkText.enabled = false;
            }

            await LMotion.Create(1.0f, 0.0f, 2)
                .Bind(_fadeIn, static (v, fadeIn) =>
                {
                    var color = fadeIn.color;
                    color.a = v;
                    fadeIn.color = color;
                })
                .ToUniTask(destroyCancellationToken);
            _fadeIn.enabled = false;

            _whiteWorldActions = new WhiteWorldActions();
            _whiteWorldActions.Title.Enable();

            await UniTask.WaitUntil(
                _whiteWorldActions.Title.LoadNextScene, static action => action.WasPressedThisFrame(), cancellationToken: destroyCancellationToken);

            await _controller.LoadAsync(SceneName.Opening, Application.exitCancellationToken);
        }

        private void OnDestroy() => _whiteWorldActions.Title.Disable();
    }
}
