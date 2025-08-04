using System.Threading;
using Cysharp.Threading.Tasks;
using WhiteWorld.Domain.Entity;

namespace WhiteWorld.Domain.LifeGame.Sequences
{
    /// <summary>
    /// カード選択のシーケンス.
    /// </summary>
    public class CardSelectionSequence : ILifeGameSequence
    {
        /// <inheritdoc/>
        public LifeGameMode Mode => LifeGameMode.CardSelection;

        public async UniTask<SpaceAmount> RunAsync(CancellationToken cancellationToken)
        {
            // カードを選択して、選ばれたカードマスを返す
            throw new System.NotImplementedException();
        }
    }
}