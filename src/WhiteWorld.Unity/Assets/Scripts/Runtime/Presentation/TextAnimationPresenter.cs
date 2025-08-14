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

        private TextAnimationData _data;
        private AutoResetUniTaskCompletionSource _onFinish;

        [Inject]
        public void Initialize(TextAnimationData data, AutoResetUniTaskCompletionSource onFinish)
        {
            _data = data;
            _onFinish = onFinish;
        }

        private async UniTaskVoid Start()
        {
            var keyword = _data.Keyword.KeywordText;
            using var dummy = _data.DummyModels.AsValueEnumerable()
                .Select(static x => x.DummyText)
                .ToArrayPool();

            await _textAnimator.StartTextAnimationAsync(keyword, dummy.Array);
            _onFinish.TrySetResult();
        }
    }
}