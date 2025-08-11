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

        private readonly ISpaceMoveController _moveController;

        public SpaceMoveSequence(ISpaceMoveController moveController)
        {
            _moveController = moveController;
        }

        /// <summary>
        /// 移動アニメーションこみの待機.
        /// </summary>
        /// <param name="spaceAmount">移動量.</param>
        /// <param name="cancellationToken"></param>
        public async UniTask<Space> MoveAsync(SpaceAmount spaceAmount, CancellationToken cancellationToken)
        {
            await UniTask.WaitForSeconds(1, cancellationToken: cancellationToken);
            // 実際に移動して、移動後のマス目種別を取得する
            return await _moveController.MoveAsync(spaceAmount, cancellationToken);
        }
    }
}