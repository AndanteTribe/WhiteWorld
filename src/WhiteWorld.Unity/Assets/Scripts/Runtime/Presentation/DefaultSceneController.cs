using System;
using System.Buffers;
using System.Collections.Generic;
using System.Threading;
using AndanteTribe.Utils;
using AndanteTribe.Utils.Unity.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;
using WhiteWorld.Domain;
using WhiteWorld.Domain.Entity;

namespace WhiteWorld.Presentation
{
    public class DefaultSceneController : ISceneController
    {
        private const string DefaultSceneName = "System";

        private readonly Stack<SceneName> _footprints = new();
        private readonly AsyncSemaphore _semaphore = new();

        /// <inheritdoc/>
        public SceneName ActiveScene { get; private set; }

        /// <inheritdoc/>
        public UniTask LoadAsync(SceneName sceneName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (sceneName == SceneName.Invalid)
            {
                throw new ArgumentException("Invalid scene name.", nameof(sceneName));
            }

            return LoadInternalAsync(sceneName, cancellationToken);
        }

        /// <inheritdoc/>
        public async UniTask LoadAsync(SceneName sceneName, object[] bindings, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (sceneName == SceneName.Invalid)
            {
                throw new ArgumentException("Invalid scene name.", nameof(sceneName));
            }

            using var _ = LifetimeScope.Enqueue(builder =>
            {
                foreach (var instance in bindings.AsSpan())
                {
                    builder.RegisterInstance(instance, instance.GetType()).AsImplementedInterfaces();
                }
            });
            await LoadInternalAsync(sceneName, cancellationToken);
        }

        private async UniTask LoadInternalAsync(SceneName sceneName, CancellationToken cancellationToken)
        {
            using var _ = await _semaphore.WaitOneAsync(cancellationToken);
            foreach (var flag in sceneName)
            {
                if (!ActiveScene.HasBitFlags(flag))
                {
                    await SceneManager.LoadSceneAsync(flag.GetEnumMemberValue(), LoadSceneMode.Additive)!
                        .WithCancellation(cancellationToken);
                }
            }

            using var __ = ListPool<UniTask>.Get(out var list);
            foreach (var flag in ActiveScene)
            {
                if (!sceneName.HasBitFlags(flag))
                {
                    list.Add(
                        SceneManager.UnloadSceneAsync(flag.GetEnumMemberValue())!.WithCancellation(
                            cancellationToken));
                }
            }

            await UniTask.WhenAll(list);
            _footprints.Push(sceneName);
            ActiveScene = sceneName;
        }

        /// <inheritdoc/>
        public async UniTask UnloadAllAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using var _ = await _semaphore.WaitOneAsync(cancellationToken);
            var array = ArrayPool<UniTask>.Shared.Rent(_footprints.Count);
            for (int i = 0; i < _footprints.Count; i++)
            {
                SceneName sceneName = _footprints.Pop();
                array[i] = UniTask.Defer((name: sceneName.GetEnumMemberValue(), token: cancellationToken),
                    static args => SceneManager.UnloadSceneAsync(args.name)!.WithCancellation(args.token));
            }
            await UniTask.WhenAll(array);
            ActiveScene = SceneName.Invalid;
            ArrayPool<UniTask>.Shared.Return(array);
        }
    }
}