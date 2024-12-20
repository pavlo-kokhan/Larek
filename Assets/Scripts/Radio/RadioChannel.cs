using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Radio
{
    [RequireComponent(typeof(AudioSource))]
    public class RadioChannel : MonoBehaviour
    {
        public AudioSource AudioSource => _audioSource;
        
        [SerializeField] private List<AudioClip> _audioClips;
        
        private AudioSource _audioSource;
        private int _currentSongIndex;

        private void Start()
        {
            ShuffleAudioClips();
            
            _audioSource = GetComponent<AudioSource>();
            
            _currentSongIndex = 0;
            _audioSource.volume = 0f;
            _audioSource.clip = _audioClips[_currentSongIndex];
            _audioSource.time = Random.Range(0f, _audioSource.clip.length);
            _audioSource.Play();
        }

        private void Update()
        {
            if (!_audioSource.isPlaying && _audioSource.time >= _audioSource.clip.length - 0.1f)
            {
                PlayNextSong();
            }
        }

        private void PlayNextSong()
        {
            if (_currentSongIndex < _audioClips.Count - 2)
            {
                _currentSongIndex += 1;
                PlayCurrentSong();
            }
            else
            {
                ShuffleAudioClips();
                _currentSongIndex = 0;
                PlayCurrentSong();
            }
        }

        private void PlayCurrentSong()
        {
            _audioSource.clip = _audioClips[_currentSongIndex];
            _audioSource.Play();
        }
        
        private void ShuffleAudioClips()
        {
            _audioClips = _audioClips.OrderBy(x => Guid.NewGuid()).ToList();
        }
    }
}