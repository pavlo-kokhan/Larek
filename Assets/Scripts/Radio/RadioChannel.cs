using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Radio
{
    public class RadioChannel : MonoBehaviour
    {
        public event Action<RadioChannel> AudioClipSwitched;
        
        [SerializeField] private List<AudioClip> _audioClips;
        
        private int _currentAudioClipIndex;
        
        public AudioClip CurrentAudioClip => _audioClips[_currentAudioClipIndex];
        public float CurrentAudioClipTime { get; private set; }

        private void Start()
        {
            ShuffleAudioClips();
            
            _currentAudioClipIndex = 0;
            CurrentAudioClipTime = Random.Range(0f, CurrentAudioClip.length);
        }

        private void Update()
        {
            if (CurrentAudioClipTime >= CurrentAudioClip.length - 0.1f)
            {
                SwitchNextSong();
            }
            
            CurrentAudioClipTime += Time.deltaTime;
        }

        private void SwitchNextSong()
        {
            if (_currentAudioClipIndex < _audioClips.Count - 2)
            {
                _currentAudioClipIndex++;
            }
            else
            {
                ShuffleAudioClips();
                _currentAudioClipIndex = 0;
            }

            CurrentAudioClipTime = 0f;
            AudioClipSwitched?.Invoke(this);
        }

        private void ShuffleAudioClips()
        {
            if (_audioClips.Count < 2) return;
            
            var random = new System.Random();
            _audioClips = _audioClips.OrderBy(x => random.Next()).ToList();
        }
    }
}