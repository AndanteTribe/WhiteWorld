using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace CardSelectFlow.Interface
{
    public interface IAppearCardDecisionAlgorithm
    {
        List<int> GetAppearCards();
    }
}