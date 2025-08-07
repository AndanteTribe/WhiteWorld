using System.Threading;
using Cysharp.Threading.Tasks;
using WhiteWorld.Domain;
using WhiteWorld.Domain.Entity;

namespace WhiteWorld.AppMain
{
    public interface ICardSelectionSequence : ILifeGameSequence
    {
        public UniTask<SpaceAmount> RunAsync(CancellationToken cancellationToken);
        public void FinishCardSelect(SpaceAmount amount);
    }
}