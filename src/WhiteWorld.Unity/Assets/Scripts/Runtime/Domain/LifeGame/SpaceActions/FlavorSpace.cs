using WhiteWorld.Domain.Entity;

namespace WhiteWorld.Domain.LifeGame.SpaceActions
{
    /// <summary>
    /// 人生ゲームのフレーバーテキストマスのマス目効果.
    /// </summary>
    public class FlavorSpace : ISpaceAction
    {
        /// <inheritdoc/>
        public Space Space => Space.Flavor;

        /// <inheritdoc/>
        public void Execute(SpaceAmount moveCount) => throw new System.NotImplementedException();
    }
}