using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MasterMemory.Tables;
using WhiteWorld.Domain.Entity;
using ZLinq;

namespace WhiteWorld.Domain.LifeGame.SpaceActions
{
    /// <summary>
    /// 人生ゲームのゴールマスのマス目効果.
    /// </summary>
    public class GoalSpace : ISpaceAction
    {
        private readonly ISpaceTelevisionController _televisionController;
        private readonly ISceneController _sceneController;
        private readonly MessageModelTable _messageTable;
        private const string MonologueIndex = "lifegame_06_";
        private int _tvMessageIndex = 3;
        /// <inheritdoc/>
        public Space Space => Space.Goal;

        public GoalSpace(
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

            // 第三章の再生
            using var tvMessages1 = _messageTable.GetRawDataUnsafe().AsValueEnumerable()
                .Where(x => x.Id.Contains($"novel_{_tvMessageIndex:00}_", StringComparison.Ordinal))
                .ToArrayPool();

            var data = new MessagePlayData(tvMessages1.Memory, true, true);
            var uts = AutoResetUniTaskCompletionSource.Create();
            await _sceneController.LoadAsync(SceneName.LifeGame | SceneName.MessageWindow,
                new object[] { data, uts }, cancellationToken);
            await uts.Task;
            await _sceneController.LoadAsync(SceneName.LifeGame, cancellationToken);
            _tvMessageIndex++;

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

            // 第四章の再生
            using var tvMessages2 = _messageTable.GetRawDataUnsafe().AsValueEnumerable()
                .Where(x => x.Id.Contains($"novel_{_tvMessageIndex:00}_", StringComparison.Ordinal))
                .ToArrayPool();

            data = new MessagePlayData(tvMessages2.Memory, true);
            uts = AutoResetUniTaskCompletionSource.Create();
            await _sceneController.LoadAsync(SceneName.LifeGame | SceneName.MessageWindow,
                new object[] { data, uts }, cancellationToken);
            await uts.Task;
            await _sceneController.LoadAsync(SceneName.LifeGame, cancellationToken);
        }
    }
}