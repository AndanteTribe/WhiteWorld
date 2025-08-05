using System.Threading;
using Cysharp.Threading.Tasks;
using WhiteWorld.Domain.Entity;

namespace WhiteWorld.AppMain
{
    public interface ICardSelectionSequence
    {
        public UniTask<SpaceAmount> RunAsync(CancellationToken cancellationToken);
        public void FinishCardSelect(SpaceAmount amount);
    }
}