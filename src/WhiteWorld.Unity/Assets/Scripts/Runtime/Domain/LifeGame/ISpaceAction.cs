using System.Threading;
using Cysharp.Threading.Tasks;
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
        /// <param name="cancellationToken">キャンセルトークン.</param>
        UniTask ExecuteAsync(CancellationToken cancellationToken);
    }
}