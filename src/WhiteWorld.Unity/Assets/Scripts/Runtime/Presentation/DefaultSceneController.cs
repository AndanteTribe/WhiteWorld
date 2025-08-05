using System.Buffers;
using System.Collections.Generic;
using System.Threading;
using AndanteTribe.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using WhiteWorld.Domain;
using WhiteWorld.Domain.Entity;

namespace WhiteWorld.Presentation
{
    public class DefaultSceneController : ISceneController
    {
        private const string DefaultSceneName = "System";

        private readonly Stack<SceneName> _sceneStack = new();

        /// <inheritdoc/>
        public SceneName CurrentScene { get; private set; }

        /// <inheritdoc/>
        public async UniTask LoadAsync(SceneName sceneName, CancellationToken cancellationToken)
        {
            // TODO: あとでデバッグ用のシーン遷移実装追加する.

            if (sceneName == SceneName.Invalid)
            {
                throw new System.ArgumentException("Invalid scene name.", nameof(sceneName));
            }

            int count = default;
            foreach (var memberValue in SceneNameEnumExtensions.GetValues())
            {
                if (memberValue == SceneName.Invalid)
                {
                    continue;
                }

                if (sceneName.HasBitFlags(memberValue))
                {
                    count++;
                    if (!_sceneStack.Contains(memberValue))
                    {
                        var name = memberValue.GetEnumMemberValue();
                        await SceneManager.LoadSceneAsync(memberValue.GetEnumMemberValue(), LoadSceneMode.Additive)!.WithCancellation(cancellationToken);
                        _sceneStack.Push(memberValue);
                    }
                }
            }

            if (count > 1)
            {
                CurrentScene = sceneName;
            }
        }

        /// <inheritdoc/>
        public async UniTask UnloadAllAsync(CancellationToken cancellationToken)
        {
            var array = ArrayPool<UniTask>.Shared.Rent(_sceneStack.Count);
            for (int i = 0; i < _sceneStack.Count; i++)
            {
                SceneName sceneName = _sceneStack.Pop();
                array[i] = UniTask.Defer((name: sceneName.GetEnumMemberValue(), token: cancellationToken),
                    static args => SceneManager.UnloadSceneAsync(args.name)!.WithCancellation(args.token));
            }
            await UniTask.WhenAll(array);
            CurrentScene = SceneName.Invalid;
        }
    }
}