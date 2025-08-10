using System.Threading;
using Cysharp.Threading.Tasks;
using WhiteWorld.Domain.Entity;

namespace WhiteWorld.Domain
{
    /// <summary>
    /// シーン遷移を制御するインターフェース.
    /// </summary>
    public interface ISceneController
    {
        /// <summary>
        /// 現在のシーン名.
        /// </summary>
        SceneName ActiveScene { get; }

        /// <summary>
        /// シーンを非同期でロードする.
        /// </summary>
        /// <param name="sceneName">読み込むシーン名.</param>
        /// <param name="cancellationToken">キャンセルトークン.</param>
        UniTask LoadAsync(SceneName sceneName, CancellationToken cancellationToken);

        /// <summary>
        /// シーンを非同期でロードする
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="bindings"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        UniTask LoadAsync(SceneName sceneName, object[] bindings, CancellationToken cancellationToken);

        /// <summary>
        /// 表示されているシーンを非同期でアンロードする.
        /// </summary>
        /// <param name="cancellationToken">キャンセルトークン.</param>
        UniTask UnloadAllAsync(CancellationToken cancellationToken);
    }
}