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

        /// <inheritdoc/>
        public void Execute(SpaceAmount moveCount)
        {
            // ReturnSpaceの効果は、実装されていません。
            // 必要に応じて、ここにReturnSpaceの効果を実装してください。
            throw new System.NotImplementedException();
        }
    }
}