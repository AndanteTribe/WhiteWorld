using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using WhiteWorld.Domain.Entity;
using ZLinq;
using MasterMemory.Tables;

namespace WhiteWorld.Domain.LifeGame.SpaceActions
{
    /// <summary>
    /// 人生ゲームのテレビマスのマス目効果.
    /// </summary>
    public class TelevisionSpace : ISpaceAction
    {
        /// <inheritdoc/>
        public Space Space => Space.Television;

        private readonly ISpaceTelevisionController _televisionController;
        private readonly ISceneController _sceneController;
        private readonly MessageModelTable _messageTable;
        // オープニングの時に01は再生しているので02から
        private readonly int _tvMessageIndex = 2;
        private const string MonologueIndex = "lifegame_05_";

        public TelevisionSpace(
            ISpaceTelevisionController televisionController,
            ISceneController sceneController,
            MessageModelTable messageTable)
        {
            _televisionController = televisionController;
            _sceneController = sceneController;
            _messageTable = messageTable;
        }

        /// <inheritdoc/>
        public async UniTask ExecuteAsync(CancellationToken cancellationToken)
        {
            await _televisionController.ExecuteAsync(cancellationToken);

            // 第二章の再生
            using var tvMessages = _messageTable.GetRawDataUnsafe().AsValueEnumerable()
                .Where(x => x.Id.Contains($"novel_{_tvMessageIndex:00}_", StringComparison.Ordinal))
                .ToArrayPool();

            var data = new MessagePlayData(tvMessages.Memory, true, true);
            var uts = AutoResetUniTaskCompletionSource.Create();
            await _sceneController.LoadAsync(SceneName.LifeGame | SceneName.MessageWindow,
                new object[] { data, uts }, cancellationToken);
            await uts.Task;
            await _sceneController.LoadAsync(SceneName.LifeGame, cancellationToken);

            // テレビマスの独白メッセージの再生
            using var monologues = _messageTable.GetRawDataUnsafe().AsValueEnumerable()
                .Where(static x => x.Id.Contains(MonologueIndex, StringComparison.Ordinal))
                .ToArrayPool();

            data = new MessagePlayData(monologues.Memory, false);
            uts = AutoResetUniTaskCompletionSource.Create();
            await _sceneController.LoadAsync(SceneName.LifeGame | SceneName.MessageWindow,
                new object[] { data, uts }, cancellationToken);
            await uts.Task;
            await _sceneController.LoadAsync(SceneName.LifeGame, cancellationToken);
        }
    }
}