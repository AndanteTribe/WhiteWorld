using System.Threading;
using System.Threading.Tasks;
using AndanteTribe.Utils;
using WhiteWorld.Domain.Entity;
using WhiteWorld.Domain.LifeGame.Sequences;

namespace WhiteWorld.Domain.LifeGame
{
    public class LifeGameMainSequence
    {
        private readonly CardSelectionSequence _cardSelectionSequence;
        private readonly SpaceMoveSequence _spaceMoveSequence;
        private readonly SpaceActionSequence _spaceActionSequence;

        public LifeGameMainSequence(
            CardSelectionSequence cardSelectionSequence,
            SpaceMoveSequence spaceMoveSequence,
            SpaceActionSequence spaceActionSequence
            )
        {
            _cardSelectionSequence = cardSelectionSequence;
            _spaceMoveSequence = spaceMoveSequence;
            _spaceActionSequence = spaceActionSequence;
          }

        public async ValueTask InitializeAsync(CancellationToken cancellationToken)
        {
            var i =await _cardSelectionSequence.RunAsync(cancellationToken);
            return;

            while (!cancellationToken.IsCancellationRequested)
            {
                var amount = await _cardSelectionSequence.RunAsync(cancellationToken);
                await _spaceMoveSequence.MoveAsync(amount, cancellationToken);
                await _spaceActionSequence.RunAsync(amount, cancellationToken);
            }
        }
    }
}