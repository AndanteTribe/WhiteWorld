using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using WhiteWorld.Domain.Entity;

namespace WhiteWorld.Domain
{
    public interface IAppearCardDecisionAlgorithm
    {
        List<SpaceAmount> GetAppearCards();
    }
}