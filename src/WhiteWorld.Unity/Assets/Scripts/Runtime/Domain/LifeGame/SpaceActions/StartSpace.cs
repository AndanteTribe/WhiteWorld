using WhiteWorld.Domain.Entity;

namespace WhiteWorld.Domain.LifeGame.SpaceActions
{
    /// <summary>
    /// 人生ゲームのスタートマスのマス目効果.
    /// </summary>
    public class StartSpace : ISpaceAction
    {
        /// <inheritdoc/>
        public Space Space => Space.Start;

        /// <inheritdoc/>
        public void Execute(SpaceAmount moveCount) => throw new System.NotImplementedException();
    }
}