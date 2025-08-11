using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using WhiteWorld.Domain.Entity;
using ZLinq;

namespace WhiteWorld.Domain.LifeGame.Sequences
{
    /// <summary>
    /// マスメ効果シーケンス.
    /// </summary>
    public class SpaceActionSequence : ILifeGameSequence
    {
        /// <inheritdoc/>
        public LifeGameMode Mode => LifeGameMode.SpaceAction;

        private readonly IReadOnlyList<ISpaceAction> _spaceActions;

        public SpaceActionSequence(IReadOnlyList<ISpaceAction> spaceActions)
        {
            _spaceActions = spaceActions;
        }

        public async UniTask RunAsync(Space space, CancellationToken cancellationToken)
        {
            var action = _spaceActions
                .AsValueEnumerable()
                .FirstOrDefault(x => x.Space == space);
            await (action?.ExecuteAsync(cancellationToken) ?? UniTask.CompletedTask);
        }
    }
}