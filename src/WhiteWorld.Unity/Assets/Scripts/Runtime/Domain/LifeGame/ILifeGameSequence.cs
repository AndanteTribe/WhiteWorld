using WhiteWorld.Domain.Entity;

namespace WhiteWorld.Domain
{
    /// <summary>
    /// 人生ゲームの各シーケンス.
    /// </summary>
    public interface ILifeGameSequence
    {
        /// <summary>
        /// 人生ゲームのモード.
        /// </summary>
        LifeGameMode Mode { get; }
    }
}