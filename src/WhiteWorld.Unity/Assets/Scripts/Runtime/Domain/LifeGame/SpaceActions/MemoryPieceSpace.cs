using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using WhiteWorld.Domain.Entity;
using ZLinq;
using MasterMemory.Tables;

namespace WhiteWorld.Domain.LifeGame.SpaceActions
{
    /// <summary>
    /// 人生ゲームの記憶のかけらマスのマス目効果.
    /// </summary>
    public class MemoryPieceSpace : ISpaceAction
    {
        /// <inheritdoc/>
        public Space Space => Space.MemoryPiece;

        private readonly KeywordModelTable _keywordTable;
        private readonly ISceneController _sceneController;

        public MemoryPieceSpace(KeywordModelTable keywordTable, ISceneController sceneController)
        {
            _keywordTable = keywordTable;
            _sceneController = sceneController;
        }

        /// <inheritdoc/>
        public async UniTask ExecuteAsync(CancellationToken cancellationToken)
        {
            await LoadTextAnimAsync(cancellationToken);
            await _sceneController.LoadAsync(SceneName.LifeGame, cancellationToken);
        }

        private async UniTask LoadTextAnimAsync(CancellationToken cancellationToken)
        {
            var messages = _keywordTable.GetRawDataUnsafe().AsValueEnumerable();

            using var arrayPool = messages.ToArrayPool();

            var count = messages.Count();
            var memory = new ReadOnlyMemory<KeywordModel>(arrayPool.Array,0, count);
            var uts = AutoResetUniTaskCompletionSource.Create();

            cancellationToken.RegisterWithoutCaptureExecutionContext(static obj =>
            {
                var source = (AutoResetUniTaskCompletionSource)obj;
                source.TrySetCanceled();
            }, uts);

            await _sceneController.LoadAsync(SceneName.LifeGame | SceneName.TextAnimation,
                new object[] { memory, uts }, cancellationToken);

            await uts.Task;
        }
    }
}