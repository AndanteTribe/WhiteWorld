using System.Threading;
using Cysharp.Threading.Tasks;
using WhiteWorld.Domain.Entity;

namespace WhiteWorld.Domain.LifeGame.SpaceActions
{
    /// <summary>
    /// 人生ゲームの「xマス戻る」マスのマス目効果.
    /// </summary>
    public class ReturnSpace : ISpaceAction
    {
        /// <inheritdoc/>
        public Space Space => Space.Return;

        private readonly SpaceAmount _proceedAmount = (SpaceAmount)(-1);
        private readonly ISpaceMoveController _moveController;

        public ReturnSpace(ISpaceMoveController moveController)
        {
            _moveController = moveController;
        }

        /// <inheritdoc/>
        public async UniTask ExecuteAsync(CancellationToken cancellationToken)
        {
            await UniTask.WaitForSeconds(1, cancellationToken: cancellationToken);
            // -1マス進む
            // 実際に移動して、移動後のマス目種別を取得する
            await _moveController.MoveAsync(_proceedAmount, cancellationToken);
        }
    }
}