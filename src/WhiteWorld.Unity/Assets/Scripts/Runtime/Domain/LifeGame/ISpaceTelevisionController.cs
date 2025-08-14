using System.Threading;
using Cysharp.Threading.Tasks;

namespace WhiteWorld.Domain.LifeGame
{
    public interface ISpaceTelevisionController
    {
        UniTask ExecuteAsync(CancellationToken cancellationToken);

        void BindCameraToPlayer();
    }
}