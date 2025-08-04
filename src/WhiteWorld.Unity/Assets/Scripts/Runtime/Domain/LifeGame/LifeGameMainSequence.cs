using System.Threading;
using System.Threading.Tasks;
using AndanteTribe.Utils;
using WhiteWorld.Domain.Entity;
using WhiteWorld.Domain.LifeGame.Sequences;

namespace WhiteWorld.Domain.LifeGame
{
    public class LifeGameMainSequence : IInitializable
    {
        private readonly CardSelectionSequence _cardSelectionSequence;
        private readonly SpaceMoveSequence _spaceMoveSequence;
        private readonly SpaceActionSequence _spaceActionSequence;

        /// <inheritdoc/>
        public async ValueTask InitializeAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var amount = await _cardSelectionSequence.RunAsync(cancellationToken);
                await _spaceMoveSequence.MoveAsync(amount, cancellationToken);
                await _spaceActionSequence.RunAsync(amount, cancellationToken);
            }
        }
    }
}