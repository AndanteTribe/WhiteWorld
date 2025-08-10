using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.Playables;
using WhiteWorld.Domain;
using Unity.Cinemachine;

namespace WhiteWorld.Presentation
{
    public class TVController : MonoBehaviour, ITVController
    {
        [SerializeField] private PlayableDirector? _timeline;

        public Subject<Unit> AnimPreFinished { get; } = new Subject<Unit>();

        private void Start()
        {
            var playableDirector = GetComponent<PlayableDirector>();
            var track = playableDirector.playableAsset.outputs.First();
            playableDirector.SetGenericBinding(track.sourceObject, Camera.main?.GetComponent<CinemachineBrain>());
            AnimPreFinished.AddTo(destroyCancellationToken);
        }

        public void StartTVAnimation()
        {
            if (_timeline != null)
            {
                _timeline.Play();
                WaitForAnimationEnd().Forget();
            }
        }

        private async UniTaskVoid WaitForAnimationEnd()
        {
            if (_timeline != null)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_timeline.duration - 3.0), cancellationToken: destroyCancellationToken);
                AnimPreFinished.OnNext(Unit.Default);
            }
        }

        public void EndTVAnimation()
        {
            if (_timeline != null)
            {
                _timeline.Stop();
            }
        }
    }
}