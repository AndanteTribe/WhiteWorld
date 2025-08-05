using WhiteWorld.Domain.Entity;

namespace WhiteWorld.Domain.LifeGame.SpaceActions
{
    /// <summary>
    /// 人生ゲームの記憶のかけらマスのマス目効果.
    /// </summary>
    public class MemoryPieceSpace : ISpaceAction
    {
        /// <inheritdoc/>
        public Space Space => Space.MemoryPiece;

        /// <inheritdoc/>
        public void Execute(SpaceAmount moveCount)
        {
            // メモリーピースの効果は、実装されていません。
            // 必要に応じて、ここにメモリーピースの効果を実装してください。
            throw new System.NotImplementedException();
        }
    }
}