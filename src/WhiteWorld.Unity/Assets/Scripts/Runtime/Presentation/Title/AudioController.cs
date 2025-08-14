using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using WhiteWorld.Domain.Entity;

namespace WhiteWorld.Presentation
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioController : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private AudioClip[] _audioClip;

        private void Awake() => DontDestroyOnLoad(this);

        /// <summary>
        /// 登録したインデックスの効果音を鳴らす
        /// </summary>
        /// <param name="index"></param>
        public async UniTask Play(int index)
        {
            var se = _audioClip[index];
            _audioSource.PlayOneShot(se);
            await UniTask.Delay(TimeSpan.FromSeconds(se.length));
        }
    }


}