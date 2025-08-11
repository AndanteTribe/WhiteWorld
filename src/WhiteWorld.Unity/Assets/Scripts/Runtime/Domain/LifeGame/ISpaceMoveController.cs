using System.Threading;
using Cysharp.Threading.Tasks;
using WhiteWorld.Domain.Entity;

namespace WhiteWorld.Domain.LifeGame
{
    /// <summary>
    /// 人生ゲームでプレイヤーのマス目移動制御.
    /// </summary>
    public interface ISpaceMoveController
    {
        /// <summary>
        /// 指定したマス目を移動する.
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        UniTask<Space> MoveAsync(SpaceAmount amount, CancellationToken cancellationToken);
    }
}