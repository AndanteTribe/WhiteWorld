using System.Threading;
using Cysharp.Threading.Tasks;
using WhiteWorld.Domain.Entity;

namespace WhiteWorld.Domain.LifeGame.Sequences
{
    /// <summary>
    /// マスメ効果シーケンス.
    /// </summary>
    public class SpaceActionSequence : ILifeGameSequence
    {
        /// <inheritdoc/>
        public LifeGameMode Mode => LifeGameMode.SpaceAction;

        private readonly ISpaceAction[] _spaceActions;

        public async UniTask RunAsync(SpaceAmount spaceAmount, CancellationToken cancellationToken)
        {
            // マス効果を実行する
            foreach (var action in _spaceActions)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                action.Execute(spaceAmount);
            }
        }
    }
}