using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using WhiteWorld.AppMain;
using WhiteWorld.Domain.Entity;

namespace WhiteWorld.Presentation.Runtime.Presentation.CardSelectFlow
{
    public class DummyCardSelectionSequence : MonoBehaviour,ICardSelectionSequence
    {
        /// <inheritdoc/>
        public LifeGameMode Mode => LifeGameMode.CardSelection;

        private AutoResetUniTaskCompletionSource<SpaceAmount> _source;

        private void Start()
        {
            RunAsync(CancellationToken.None).Forget();
        }

        public async UniTask<SpaceAmount> RunAsync(CancellationToken cancellationToken)
        {
            _source = AutoResetUniTaskCompletionSource<SpaceAmount>.Create();

            //キャンセル処理
            cancellationToken.RegisterWithoutCaptureExecutionContext(static s =>
            {
                var source = (AutoResetUniTaskCompletionSource<SpaceAmount>)s;
                source.TrySetCanceled();
            }, _source);

            var result = await _source.Task;

            _source = null;

            return result;
        }

        public void FinishCardSelect(SpaceAmount amount)
        {
            _source?.TrySetResult(amount);
        }
    }
}