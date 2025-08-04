using WhiteWorld.Domain.Entity;

namespace WhiteWorld.Domain.LifeGame
{
    /// <summary>
    /// マスメ効果.
    /// </summary>
    public interface ISpaceAction
    {
        /// <summary>
        /// マス種別.
        /// </summary>
        Space Space { get; }

        /// <summary>
        /// 実行する.
        /// </summary>
        /// <param name="moveCount">移動するマス目量.</param>
        void Execute(SpaceAmount moveCount);
    }
}