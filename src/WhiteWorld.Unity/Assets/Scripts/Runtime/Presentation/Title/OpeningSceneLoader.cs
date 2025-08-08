using System.Threading;
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

        private void Awake()
        {
            _whiteWorldActions = new WhiteWorldActions();
            _whiteWorldActions.Title.Enable();
        }

        private void Update()
        {
            if (_whiteWorldActions.Title.LoadNextScene.WasPressedThisFrame())
            {
                LoadSceneAsync().Forget();
            }
        }

        private async UniTaskVoid LoadSceneAsync()
        {
            await _controller.UnloadAllAsync(CancellationToken.None);
            await _controller.LoadAsync(SceneName.Opening, CancellationToken.None);
        }

        private void OnDestroy() => _whiteWorldActions.Title.Disable();
    }
}
