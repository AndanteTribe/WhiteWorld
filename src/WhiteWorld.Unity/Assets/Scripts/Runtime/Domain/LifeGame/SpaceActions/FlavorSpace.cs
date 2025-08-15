using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using WhiteWorld.Domain.Entity;
using ZLinq;
using MasterMemory.Tables;

namespace WhiteWorld.Domain.LifeGame.SpaceActions
{
    /// <summary>
    /// 人生ゲームのフレーバーテキストマスのマス目効果.
    /// </summary>
    public class FlavorSpace : ISpaceAction
    {
        /// <inheritdoc/>
        public Space Space => Space.Flavor;

        private readonly MessageModelTable _messageTable;
        private readonly ISceneController _sceneController;
        private readonly int _maxFlavor;
        private readonly Random _random = new Random();

        public FlavorSpace(MessageModelTable messageTable, ISceneController sceneController)
        {
            _messageTable = messageTable;
            // flavor_01, flavor_02, ..., flavor_99 のようなIDを持つエンティティの数をカウント
            _maxFlavor = messageTable.GetRawDataUnsafe().AsValueEnumerable()
                .Where(static x => x.Id.AsSpan().Contains("flavor_", StringComparison.Ordinal))
                .Select(static x => int.Parse(x.Id.AsSpan(7, 2)))
                .Distinct().Count();
            _sceneController = sceneController;
        }

        /// <inheritdoc/>
        public async UniTask ExecuteAsync(CancellationToken cancellationToken)
        {
            var r = _random.Next(1, _maxFlavor + 1);
            using var message = _messageTable.GetRawDataUnsafe().AsValueEnumerable()
                .Where(x => x.Id.AsSpan().Contains($"flavor_{r:00}", StringComparison.Ordinal))
                .ToArrayPool();

            var data = new MessagePlayData(message.Memory, true, true);
            var uts = AutoResetUniTaskCompletionSource.Create();
            await _sceneController.LoadAsync(SceneName.LifeGame | SceneName.MessageWindow,
                new object[] { data, uts }, cancellationToken);
            await uts.Task;
            await _sceneController.LoadAsync(SceneName.LifeGame, cancellationToken);
        }
    }
}