using System.Threading;
using Cysharp.Threading.Tasks;
using WhiteWorld.Domain.Entity;

namespace WhiteWorld.Domain.LifeGame.Sequences
{
    /// <summary>
    /// マスメ移動シーケンス.
    /// </summary>
    public class SpaceMoveSequence : ILifeGameSequence
    {
        /// <inheritdoc/>
        public LifeGameMode Mode => LifeGameMode.SpaceMove;

        /// <summary>
        /// 移動アニメーションこみの待機.
        /// </summary>
        /// <param name="spaceAmount">移動量.</param>
        /// <param name="cancellationToken"></param>
        public async UniTask MoveAsync(SpaceAmount spaceAmount, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}