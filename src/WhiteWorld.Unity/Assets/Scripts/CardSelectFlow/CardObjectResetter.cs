using CardSelectFlow.Interface;
using R3;
using VContainer;
using VContainer.Unity;

namespace CardSelectFlow
{
    /// <summary>
    /// CardObjectManagerをリセットするためのクラス
    /// 循環参照を避けるために苦肉の策
    /// </summary>
    public class CardObjectResetter : IStartable
    {
        [Inject]
        private ICardSelectFlowController _controller;
        [Inject]
        private ICardObjectManager _manager;

        //フロー開始時にカードをリセット
        public void Start() =>
            _controller.OnStartCardSelectFlow
                .Subscribe(_ => Reset());

        private void Reset() => _manager.Reset();
    }

}