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
        public void Execute(SpaceAmount moveCount) => throw new System.NotImplementedException();
    }
}