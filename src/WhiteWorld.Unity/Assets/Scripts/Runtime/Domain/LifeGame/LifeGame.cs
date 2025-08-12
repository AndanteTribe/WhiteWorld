using System;
using System.Threading;
using System.Threading.Tasks;
using AndanteTribe.Utils;
using Cysharp.Threading.Tasks;
using MasterMemory.Tables;
using WhiteWorld.Domain.Entity;
using ZLinq;

namespace WhiteWorld.Domain.LifeGame
{
    public class LifeGame : IInitializable
    {
        private readonly ISceneController _sceneController;
        private readonly MessageModelTable _messageTable;
        private readonly LifeGameMainSequence _mainSequence;

        public LifeGame(
            ISceneController sceneController,
            LifeGameMainSequence mainSequence,
            MessageModelTable messageTable)
        {
            _sceneController = sceneController;
            _mainSequence = mainSequence;
            _messageTable = messageTable;
        }

        public async ValueTask InitializeAsync(CancellationToken cancellationToken)
        {
            // 少しディレイ
            await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: cancellationToken);

            // 人生ゲームシーン読み込み直後のメッセージ再生
            using (var messages = _messageTable.GetRawDataUnsafe()
                       .AsValueEnumerable()
                       .Where(static x => x.Id.AsSpan().Contains($"lifegame_{(byte)LifeGameTutorialID.SceneStart:00}_", StringComparison.Ordinal))
                       .ToArrayPool())
            {
                var data = new MessagePlayData(messages.Memory);
                var uts = AutoResetUniTaskCompletionSource.Create();
                await _sceneController.LoadAsync(SceneName.LifeGame | SceneName.MessageWindow,
                    new object[] { data, uts }, cancellationToken);
                await uts.Task;
            }

            await _mainSequence.InitializeAsync(cancellationToken);
            await _mainSequence.RunLoopAsync(cancellationToken);
        }
    }
}