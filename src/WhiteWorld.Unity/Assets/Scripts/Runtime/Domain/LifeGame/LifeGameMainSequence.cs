using System;
using System.Threading;
using AndanteTribe.Utils;
using Cysharp.Threading.Tasks;
using MasterMemory.Tables;
using WhiteWorld.Domain.Entity;
using WhiteWorld.Domain.LifeGame.Sequences;
using ZLinq;

namespace WhiteWorld.Domain.LifeGame
{
    public class LifeGameMainSequence
    {
        private readonly CardSelectionSequence _cardSelectionSequence;
        private readonly SpaceMoveSequence _spaceMoveSequence;
        private readonly SpaceActionSequence _spaceActionSequence;
        private readonly ISceneController _sceneController;
        private readonly MessageModelTable _messageTable;

        public LifeGameMainSequence(
            CardSelectionSequence cardSelectionSequence,
            SpaceMoveSequence spaceMoveSequence,
            SpaceActionSequence spaceActionSequence,
            ISceneController sceneController,
            MessageModelTable messageTable)
        {
            _cardSelectionSequence = cardSelectionSequence;
            _spaceMoveSequence = spaceMoveSequence;
            _spaceActionSequence = spaceActionSequence;
            _sceneController = sceneController;
            _messageTable = messageTable;
        }

        public async UniTask InitializeAsync(CancellationToken cancellationToken)
        {
            CardSelectTutorialAsync(cancellationToken).Forget();
            var spaceAmount = await _cardSelectionSequence.RunAsync(cancellationToken);
            var space = await _spaceMoveSequence.MoveAsync(spaceAmount, cancellationToken);
            await TutorialAsync(LifeGameTutorialID.BeforeSpaceAction, cancellationToken);
            await _spaceActionSequence.RunAsync(space, cancellationToken);
            await TutorialAsync(LifeGameTutorialID.AfterSpaceAction, cancellationToken);
        }

        public async UniTask RunLoopAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var spaceAmount = await _cardSelectionSequence.RunAsync(cancellationToken);
                var space = await _spaceMoveSequence.MoveAsync(spaceAmount, cancellationToken);
                await _spaceActionSequence.RunAsync(space, cancellationToken);
            }
        }

        private async UniTask CardSelectTutorialAsync(CancellationToken cancellationToken)
        {
            // カード選択画面が表示されるまで待機
            await UniTask.WaitUntil(_sceneController, static controller => controller.ActiveScene.HasBitFlags(SceneName.CardSelectEdit), cancellationToken: cancellationToken);
            await TutorialAsync(LifeGameTutorialID.CardSelect, cancellationToken);
        }

        private async UniTask TutorialAsync(LifeGameTutorialID tutorialId, CancellationToken cancellationToken)
        {
            using var messages = _messageTable.GetRawDataUnsafe()
                .AsValueEnumerable()
                .Where(x => x.Id.AsSpan().Contains($"lifegame_{(byte)tutorialId:00}_", StringComparison.Ordinal))
                .ToArrayPool();
            var data = new MessagePlayData(messages.Memory);
            var uts = AutoResetUniTaskCompletionSource.Create();
            var currentScene = _sceneController.ActiveScene;
            await _sceneController.LoadAsync(currentScene | SceneName.MessageWindow, new object[] { data, uts }, cancellationToken);
            await uts.Task;
            await _sceneController.LoadAsync(currentScene, cancellationToken);
        }
    }
}