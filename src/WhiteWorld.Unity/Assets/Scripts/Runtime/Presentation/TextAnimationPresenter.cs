using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using WhiteWorld.Domain.Entity;
using ZLinq;

namespace WhiteWorld.Presentation.Runtime
{
    public class TextAnimationPresenter : MonoBehaviour
    {
        [SerializeField]
        private TextAnimator _textAnimator;

        private KeywordModel[] _data;
        private AutoResetUniTaskCompletionSource _onFinish;

        [Inject]
        public void Initialize(KeywordModel[] data, AutoResetUniTaskCompletionSource onFinish)
        {
            _data = data;
            _onFinish = onFinish;
        }

        private async void Start()
        {
            var keyword = _data[0].KeywordText;
            var dummyText = _data[0].DummyData.ToArray();
            var dummy = dummyText.AsValueEnumerable()
                .Select(x => x.DummyText)
                .ToArray();

            await _textAnimator.StartTextAnimationAsync(keyword, dummy);
        }
    }
}