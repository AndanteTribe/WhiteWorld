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
        private readonly DummyModelTable _dummyTable;
        private readonly ISceneController _sceneController;

        private readonly Random _random = new Random();

        public MemoryPieceSpace(KeywordModelTable keywordTable, DummyModelTable dummyTable ,ISceneController sceneController)
        {
            _keywordTable = keywordTable;
            _dummyTable = dummyTable;
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
            // ランダムにキーワードを選ぶ
            var keyword = _keywordTable.GetRawDataUnsafe()[_random.Next(0, _keywordTable.Count)];
            var data = new TextAnimationData(keyword, new ReadOnlyMemory<DummyModel>(_dummyTable.GetRawDataUnsafe()));
            var uts = AutoResetUniTaskCompletionSource.Create();

            cancellationToken.RegisterWithoutCaptureExecutionContext(static obj =>
            {
                var source = (AutoResetUniTaskCompletionSource)obj;
                source.TrySetCanceled();
            }, uts);

            await _sceneController.LoadAsync(SceneName.LifeGame | SceneName.TextAnimation,
                new object[] { data, uts }, cancellationToken);

            await uts.Task;
        }
    }
}