using System;
using System.Threading;
using System.Threading.Tasks;
using AndanteTribe.Utils;
using Cysharp.Threading.Tasks;
using R3;
using WhiteWorld.Domain.Entity;
using ZLinq;

namespace WhiteWorld.Domain.Opening
{
    public class Opening : IInitializable
    {
        private readonly ISceneController _sceneController;
        private readonly ITVController _tvController;
        private readonly IPlayerControl _playerControl;
        private readonly IMasterDataRepository<MessageModel> _messageRepository;

        public Opening(
            ISceneController sceneController,
            ITVController tvController,
            IPlayerControl control,
            IMasterDataRepository<MessageModel> messageRepository
            )
        {
            _sceneController = sceneController;
            _tvController = tvController;
            _playerControl = control;
            _messageRepository = messageRepository;
        }

        public async ValueTask InitializeAsync(CancellationToken cancellationToken)
        {
            // 一度、プレイヤーは動けないようにする
            _playerControl.CanMove = false;
            // オープニングメッセージ
            using (var messages = _messageRepository.Entities
                       .AsValueEnumerable()
                       .Where(static x => x.Id.AsSpan().Contains("opening_01_", StringComparison.Ordinal))
                       .ToArrayPool())
            {
                var data = new MessagePlayData(messages.Memory);
                var uts = AutoResetUniTaskCompletionSource.Create();
                await _sceneController.LoadAsync(SceneName.Opening | SceneName.MessageWindow, new object[]{ data, uts }, cancellationToken);
                await uts.Task;
                await _sceneController.LoadAsync(SceneName.Opening, cancellationToken);
            }

            _playerControl.CanMove = true;
            await _tvController.WaitForAnimationPreEndAsync(cancellationToken);

            // ノベル第一章
            using (var messages = _messageRepository.Entities
                       .AsValueEnumerable()
                       .Where(static x => x.Id.AsSpan().Contains("novel_01_", StringComparison.Ordinal))
                       .ToArrayPool())
            {
                var data = new MessagePlayData(messages.Memory, true);
                var uts = AutoResetUniTaskCompletionSource.Create();
                await _sceneController.LoadAsync(SceneName.Opening | SceneName.MessageWindow, new object[]{ data, uts }, cancellationToken);
                await uts.Task;
                _sceneController.LoadAsync(SceneName.LifeGame, CancellationToken.None).Forget();
            }
        }
    }
}