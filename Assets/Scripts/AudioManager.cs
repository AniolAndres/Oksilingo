using System;
using UnityEngine;

namespace Scripts
{
    public class AudioManager
    {
        private static AudioManager _instance;

        public static AudioManager Instance
        {
            get
            {
              _instance = _instance ?? new AudioManager();
              return _instance;
            }
        }

        public event Action<AudioType> OnAudioPlayRequest; 
        
        public void PlayAudio(AudioType audioType)
        {
            OnAudioPlayRequest?.Invoke(audioType);
        }
    }

    public enum AudioType
    {
        Click,
        Win,
        Error,
        Congratulation,
    }
}