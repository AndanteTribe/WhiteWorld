using System.Threading;
using Cysharp.Threading.Tasks;
using WhiteWorld.Domain.Entity;

namespace WhiteWorld.Domain.LifeGame.SpaceActions
{
    /// <summary>
    /// 人生ゲームのゴールマスのマス目効果.
    /// </summary>
    public class GoalSpace : ISpaceAction
    {
        /// <inheritdoc/>
        public Space Space => Space.Goal;

        /// <inheritdoc/>
        public UniTask ExecuteAsync(CancellationToken cancellationToken) => throw new System.NotImplementedException();
    }
}