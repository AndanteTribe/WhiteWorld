using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using WhiteWorld.Domain.Entity;
using ZLinq;

namespace WhiteWorld.Domain.LifeGame.SpaceActions
{
    /// <summary>
    /// 人生ゲームのフレーバーテキストマスのマス目効果.
    /// </summary>
    public class FlavorSpace : ISpaceAction
    {
        /// <inheritdoc/>
        public Space Space => Space.Flavor;

        private readonly IMasterDataRepository<MessageModel> _dataRepository;
        private readonly ISceneController _sceneController;
        private readonly int _maxFlavor;
        private readonly Random _random = new Random();

        public FlavorSpace(IMasterDataRepository<MessageModel> dataRepository, ISceneController sceneController)
        {
            _dataRepository = dataRepository;
            // flavor_01, flavor_02, ..., flavor_99 のようなIDを持つエンティティの数をカウント
            _maxFlavor = dataRepository.Entities.AsValueEnumerable()
                .Where(static x => x.Id.AsSpan().Contains("flavor_", StringComparison.Ordinal))
                .Select(static x => int.Parse(x.Id.AsSpan(7, 2)))
                .Distinct().Count();
            _sceneController = sceneController;
        }

        /// <inheritdoc/>
        public async UniTask ExecuteAsync(CancellationToken cancellationToken)
        {
            var r = _random.Next(1, _maxFlavor + 1);
            using var message = _dataRepository.Entities.AsValueEnumerable()
                .Where(x => x.Id.AsSpan().Contains($"flavor_{r:00}", StringComparison.Ordinal))
                .ToArrayPool();

            var data = new MessagePlayData(message.Memory, true);
            var uts = AutoResetUniTaskCompletionSource.Create();
            await _sceneController.LoadAsync(SceneName.LifeGame | SceneName.MessageWindow,
                new object[] { data, uts }, cancellationToken);
            await uts.Task;
            await _sceneController.LoadAsync(SceneName.LifeGame, cancellationToken);
        }
    }
}