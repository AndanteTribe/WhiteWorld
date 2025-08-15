using System.Threading;
using Cysharp.Threading.Tasks;

namespace WhiteWorld.Domain
{
    /// <summary>
    /// テレビのアニメーションを制御するインターフェース.
    /// </summary>
    public interface ITVController
    {
        /// <summary>
        /// テレビのアニメーションが終了する手前まで待機.
        /// </summary>
        UniTask WaitForAnimationPreEndAsync(CancellationToken cancellationToken = default);
        /// <summary>
        /// テレビのアニメーションが終了するまで待機.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        UniTask WaitForAnimationEndAsync(CancellationToken cancellationToken = default);
        /// <summary>
        /// テレビのアニメーションを開始する.
        /// </summary>
        void StartTVAnimation();
    }
}