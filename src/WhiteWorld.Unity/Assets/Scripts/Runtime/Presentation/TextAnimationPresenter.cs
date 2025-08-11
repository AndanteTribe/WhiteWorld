using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using WhiteWorld.Domain.Entity;
using ZLinq;
using Random = System.Random;

namespace WhiteWorld.Presentation.Runtime
{
    public class TextAnimationPresenter : MonoBehaviour
    {
        [SerializeField]
        private TextAnimator _textAnimator;

        private ReadOnlyMemory<KeywordModel> _data;
        private AutoResetUniTaskCompletionSource _onFinish;
        private readonly Random _random = new();

        [Inject]
        public void Initialize(ReadOnlyMemory<KeywordModel> data, AutoResetUniTaskCompletionSource onFinish)
        {
            _data = data;
            _onFinish = onFinish;
        }

        private async UniTaskVoid Start()
        {
            var r = _random.Next(0,_data.Length);
            var data = _data.ToArray();
            var keyword = data[r].KeywordText;
            var dummyText = data[r].DummyData.ToArray();
            var dummy = dummyText.AsValueEnumerable()
                .Select(x => x.DummyText)
                .ToArray();

            await _textAnimator.StartTextAnimationAsync(keyword, dummy);
            _onFinish.TrySetResult();
        }
    }
}