namespace WhiteWorld.Domain
{
    /// <summary>
    /// テレビのアニメーションを制御するインターフェース.
    /// </summary>
    public interface ITVController
    {
        /// <summary>
        /// テレビのアニメーションを開始する.
        /// </summary>
        void StartTVAnimation();
        /// <summary>
        /// テレビのアニメーションを終了する.
        /// </summary>
        void EndTVAnimation();
    }
}