using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;
using WhiteWorld.Domain;
using Unity.Cinemachine;

namespace WhiteWorld.Presentation
{
    public class TVController : MonoBehaviour, ITVController
    {
        [SerializeField] private PlayableDirector? _timeline;

        private void Start()
        {
            var playableDirector = GetComponent<PlayableDirector>();
            var track = playableDirector.playableAsset.outputs.First();
            playableDirector.SetGenericBinding(track.sourceObject, Camera.main?.GetComponent<CinemachineBrain>());
        }

        public void StartTVAnimation()
        {
            if (_timeline != null)
            {
                _timeline.Play();
            }
        }

        public async UniTask WaitForAnimationPreEndAsync(CancellationToken cancellationToken = default)
        {
            using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, destroyCancellationToken);
            if (_timeline != null)
            {
                // タイムラインが開始していなければ開始を待つ
                await UniTask.WaitUntil(_timeline, static timeline => timeline.state == PlayState.Playing, cancellationToken: cts.Token);
                await UniTask.Delay(TimeSpan.FromSeconds(_timeline.duration - 3.0), cancellationToken: cts.Token);
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