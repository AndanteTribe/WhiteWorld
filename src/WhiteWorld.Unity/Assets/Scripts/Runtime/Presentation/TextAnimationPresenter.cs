using System;
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

        private ReadOnlyMemory<KeywordModel> _data;
        private ReadOnlyMemory<DummyModel> _dummyModel;
        private AutoResetUniTaskCompletionSource _onFinish;

        [Inject]
        public void Initialize(ReadOnlyMemory<KeywordModel> data, ReadOnlyMemory<DummyModel> dummy, AutoResetUniTaskCompletionSource onFinish)
        {
            _data = data;
            _onFinish = onFinish;
            _dummyModel = dummy;
        }

        private async UniTaskVoid Start()
        {
            var r = UnityEngine.Random.Range(0, _data.Length);
            var data = _data.Span[r];
            var keyword = data.KeywordText;
            var dummy = _dummyModel.AsValueEnumerable()
                .Select(x => x.DummyText)
                .ToArray();

            await _textAnimator.StartTextAnimationAsync(keyword, dummy);
            _onFinish.TrySetResult();
        }
    }
}