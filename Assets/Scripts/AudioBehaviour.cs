using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts
{
    public class AudioBehaviour : MonoBehaviour
    {
        [SerializeField] private List<AudioPair> _audioPairs;
        [SerializeField] private AudioSource _audioSource;
        
        public void OnEnable()
        {
            AudioManager.Instance.OnAudioPlayRequest += PlayAudio;
        }

        private void OnDisable()
        {
            AudioManager.Instance.OnAudioPlayRequest -= PlayAudio;
        }

        private void PlayAudio(AudioType type)
        {
            var pair =  _audioPairs.First(x => x.Type == type);
            _audioSource.PlayOneShot(pair.Clip);
        }
    }

    [Serializable]
    public class AudioPair
    {
        public AudioType Type;
        public AudioClip Clip;
    }
}
