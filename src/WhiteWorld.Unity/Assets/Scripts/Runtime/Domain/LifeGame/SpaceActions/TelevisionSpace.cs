using WhiteWorld.Domain.Entity;

namespace WhiteWorld.Domain.LifeGame.SpaceActions
{
    /// <summary>
    /// 人生ゲームのテレビマスのマス目効果.
    /// </summary>
    public class TelevisionSpace : ISpaceAction
    {
        /// <inheritdoc/>
        public Space Space => Space.Television;

        /// <inheritdoc/>
        public void Execute(SpaceAmount moveCount)
        {
            // テレビの効果は、実装されていません。
            // 必要に応じて、ここにテレビの効果を実装してください。
            throw new System.NotImplementedException();
        }
    }
}