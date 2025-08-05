using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace CardSelectFlow.Interface
{
    public interface ICardAnimation
    {
        public UniTask Appear();
        public UniTask DisAppear();
    }
}