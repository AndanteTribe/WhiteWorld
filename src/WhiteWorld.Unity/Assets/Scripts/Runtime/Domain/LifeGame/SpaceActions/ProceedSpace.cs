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

        /// <inheritdoc/>
        public void Execute(SpaceAmount moveCount)
        {
            // ProceedSpaceの効果は、実装されていません。
            // 必要に応じて、ここにProceedSpaceの効果を実装してください。
            throw new System.NotImplementedException();
        }
    }
}