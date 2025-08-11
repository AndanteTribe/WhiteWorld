using System.Threading;
using Cysharp.Threading.Tasks;
using WhiteWorld.AppMain;
using WhiteWorld.Domain.Entity;

namespace WhiteWorld.Domain.LifeGame.Sequences
{
    /// <summary>
    /// カード選択のシーケンス.
    /// </summary>
    public class CardSelectionSequence : ICardSelectionSequence
    {
        /// <inheritdoc/>
        public LifeGameMode Mode => LifeGameMode.CardSelection;

        private readonly ISceneController _controller;

        private AutoResetUniTaskCompletionSource<SpaceAmount> _source;

        public CardSelectionSequence(ISceneController controller)
        {
            _controller = controller;
        }

        public async UniTask<SpaceAmount> RunAsync(CancellationToken cancellationToken)
        {
            //Scene遷移(事前にアンロード想定)
            await _controller.LoadAsync(SceneName.LifeGame | SceneName.CardSelectEdit, cancellationToken);

            _source = AutoResetUniTaskCompletionSource<SpaceAmount>.Create();

            //キャンセル処理
            cancellationToken.RegisterWithoutCaptureExecutionContext(static s =>
            {
                var source = (AutoResetUniTaskCompletionSource<SpaceAmount>)s;
                source.TrySetCanceled();
            }, _source);

            var result = await _source.Task;

            _source = null;
            await _controller.LoadAsync(SceneName.LifeGame, cancellationToken);

            return result;
        }

        public void FinishCardSelect(SpaceAmount amount)
        {
            _source?.TrySetResult(amount);
        }
    }
}