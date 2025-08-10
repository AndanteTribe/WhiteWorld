using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using WhiteWorld.Domain.Entity;
using ZLinq;

namespace WhiteWorld.Domain.LifeGame.SpaceActions
{
    /// <summary>
    /// 人生ゲームの記憶のかけらマスのマス目効果.
    /// </summary>
    public class MemoryPieceSpace : ISpaceAction
    {
        /// <inheritdoc/>
        public Space Space => Space.MemoryPiece;

        private readonly IMasterDataRepository<KeywordModel> _masterDataRepository;
        private readonly ISceneController _sceneController;

        public MemoryPieceSpace(IMasterDataRepository<KeywordModel> masterDataRepository, ISceneController sceneController)
        {
            _masterDataRepository = masterDataRepository;
            _sceneController = sceneController;
        }

        /// <inheritdoc/>
        public void Execute(SpaceAmount moveCount)
        {
            // メモリーピースの効果は、実装されていません。
            // 必要に応じて、ここにメモリーピースの効果を実装してください。

            using var messages = _masterDataRepository.Entities
                .AsValueEnumerable()
                .Where(static x => x.Id.AsSpan().Contains("01", StringComparison.Ordinal))
                .ToArrayPool();

            var uts = AutoResetUniTaskCompletionSource.Create();

            _sceneController
                .LoadAsync(SceneName.TextAnimation, new object[]{ messages.Array, uts }, CancellationToken.None)
                .Forget();
        }
    }
}