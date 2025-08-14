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
        private int _messageIndex = 2;

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
            using var messages = _messageTable.GetRawDataUnsafe().AsValueEnumerable()
                .Where(x => x.Id.Contains($"novel_{_messageIndex:00}_", StringComparison.Ordinal))
                .ToArrayPool();

            var data = new MessagePlayData(messages.Memory, true);
            var uts = AutoResetUniTaskCompletionSource.Create();
            await _sceneController.LoadAsync(SceneName.LifeGame | SceneName.MessageWindow,
                new object[] { data, uts }, cancellationToken);
            await uts.Task;
            _messageIndex++;
            await _sceneController.LoadAsync(SceneName.LifeGame, cancellationToken);

            _televisionController.BindCameraToPlayer();
        }
    }
}