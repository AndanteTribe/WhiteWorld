namespace WhiteWorld.Domain.Entity
{
    /// <summary>
    /// マスメの効果種別.
    /// </summary>
    public enum Space : byte
    {
        /// <summary>
        /// 無効種別.
        /// </summary>
        Invalid,

        /// <summary>
        /// 起点.
        /// </summary>
        Start,

        /// <summary>
        /// 終点.
        /// </summary>
        Goal,

        /// <summary>
        /// xマスすすむ.
        /// </summary>
        Proceed,

        /// <summary>
        /// xマスもどる.
        /// </summary>
        Return,

        /// <summary>
        /// テレビマス.
        /// </summary>
        Television,

        /// <summary>
        /// 記憶のかけらマス.
        /// </summary>
        MemoryPiece,

        /// <summary>
        /// フレーバーテキストマス.
        /// </summary>
        Flavor,
    }
}