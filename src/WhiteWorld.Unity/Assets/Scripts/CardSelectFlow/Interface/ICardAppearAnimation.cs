using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace CardSelectFlow.Interface
{
    public interface ICardAppearAnimation
    {
        UniTask Appear(List<int> cardNumbers);
    }
}