namespace WhiteWorld.Domain.Entity
{
    /// <summary>
    /// 人生ゲームのモード.
    /// </summary>
    public enum LifeGameMode : byte
    {
        /// <summary>
        /// カード選択.
        /// </summary>
        CardSelection,

        /// <summary>
        /// マスの移動.
        /// </summary>
        SpaceMove,

        /// <summary>
        /// マス効果.
        /// </summary>
        SpaceAction,
    }
}