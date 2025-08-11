using System.Threading;
using Cysharp.Threading.Tasks;
using WhiteWorld.Domain.Entity;

namespace WhiteWorld.Domain.LifeGame.SpaceActions
{
    /// <summary>
    /// 人生ゲームのスタートマスのマス目効果.
    /// </summary>
    public class StartSpace : ISpaceAction
    {
        /// <inheritdoc/>
        public Space Space => Space.Start;

        /// <inheritdoc/>
        public UniTask ExecuteAsync(CancellationToken cancellationToken) => throw new System.NotImplementedException();
    }
}