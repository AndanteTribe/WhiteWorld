using System.Linq;
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

        public void StartTVAnimation() => _timeline?.Play();

        public void EndTVAnimation() => _timeline?.Stop();
    }
}