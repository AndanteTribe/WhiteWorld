using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using WhiteWorld.Domain.Entity;

namespace CardSelectFlow.Interface
{
    public interface ICardObjectManager
    {
        public UniTask<SpaceAmount> WaitPlayerSelectAsync(CancellationToken token);
        public void UpdateCard();
        public void UpdateAlgorithm(IAppearCardDecisionAlgorithm algorithm);
    }
}