using System;
using R3;

namespace WhiteWorld.Domain
{
    /// <summary>
    /// テレビのアニメーションを制御するインターフェース.
    /// </summary>
    public interface ITVController
    {
        /// <summary>
        /// テレビのアニメーションが終了する手前で発行されるイベント.
        /// </summary>
        Subject<Unit> AnimPreFinished { get; }
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