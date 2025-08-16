using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace WhiteWorld.Presentation
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioController : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private AudioClip[] _audioClip;

        /// <summary>
        /// 登録したインデックスの効果音を鳴らす
        /// </summary>
        /// <param name="index"></param>
        /// <param name="token"></param>
        public async UniTask PlaySE(int index, CancellationToken token)
        {
            var se = _audioClip[index];
            _audioSource.PlayOneShot(se);
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(se.length), cancellationToken: token);
            }
            catch (OperationCanceledException)
            {
                _audioSource.Stop();
                throw;
            }
        }

        public void PlayBGM()
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
        }

        public void StopBGM()
        {
            if (_audioSource.isPlaying)
            {
                _audioSource.Stop();
            }
        }
    }


}