using Cysharp.Threading.Tasks;
using LitMotion;
using UnityEngine;
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

        [Inject]
        private ISceneController _controller;
        private WhiteWorldActions _whiteWorldActions;

        private async UniTaskVoid Start()
        {
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
