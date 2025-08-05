using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using WhiteWorld.Domain.Entity;

namespace CardSelectFlow.Interface
{
    public interface IAppearCardDecisionAlgorithm
    {
        List<SpaceAmount> GetAppearCards();
    }
}