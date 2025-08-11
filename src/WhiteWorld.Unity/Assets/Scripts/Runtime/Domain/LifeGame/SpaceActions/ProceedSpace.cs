using System.Threading;
using Cysharp.Threading.Tasks;
using WhiteWorld.Domain.Entity;

namespace WhiteWorld.Domain.LifeGame.SpaceActions
{
    /// <summary>
    /// 人生ゲームの「xマス進む」のマス目効果.
    /// </summary>
    public class ProceedSpace : ISpaceAction
    {
        /// <inheritdoc/>
        public Space Space => Space.Proceed;

        private readonly SpaceAmount _proceedAmount = (SpaceAmount)3;
        private readonly ISpaceMoveController _moveController;
        private readonly TelevisionSpace _televisionSpace;

        public ProceedSpace(ISpaceMoveController moveController, TelevisionSpace televisionSpace)
        {
            _moveController = moveController;
            _televisionSpace = televisionSpace;
        }

        /// <inheritdoc/>
        public async UniTask ExecuteAsync(CancellationToken cancellationToken)
        {
            await UniTask.WaitForSeconds(1, cancellationToken: cancellationToken);
            // 3マス進む
            // 実際に移動して、移動後のマス目種別を取得する
            var nextSpace = await _moveController.MoveAsync(_proceedAmount, cancellationToken);
            if (nextSpace == Space.Television)
            {
                await _televisionSpace.ExecuteAsync(cancellationToken);
            }
        }
    }
}