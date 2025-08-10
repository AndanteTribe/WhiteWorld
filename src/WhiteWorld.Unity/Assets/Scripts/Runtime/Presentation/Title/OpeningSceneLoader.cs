using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using WhiteWorld.Domain;
using WhiteWorld.Domain.Entity;

namespace WhiteWorld.Presentation
{
    public class OpeningSceneLoader : MonoBehaviour
    {
        [Inject]
        private ISceneController _controller;
        private WhiteWorldActions _whiteWorldActions;

        private async UniTaskVoid Start()
        {
            _whiteWorldActions = new WhiteWorldActions();
            _whiteWorldActions.Title.Enable();

            await UniTask.WaitUntil(
                _whiteWorldActions.Title.LoadNextScene, static action => action.WasPressedThisFrame(), cancellationToken: destroyCancellationToken);

            await _controller.LoadAsync(SceneName.Opening, Application.exitCancellationToken);
        }

        private void OnDestroy() => _whiteWorldActions.Title.Disable();
    }
}
