using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Radio
{
    // todo
    public class RadioPlayer : MonoBehaviour
    {
        [SerializeField] private RadioVolumeInput volumeInput;
        [SerializeField] private RadioChannelInput channelInput;
        [SerializeField] private List<AudioClip> songs;

        private AudioSource _audioSource;
        private int _currentSongIndex;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            
            ShuffleSongs();
            PlayCurrentSong();
        }

        private void OnEnable()
        {
            volumeInput.VolumeChanged += OnVolumeChanged;
            channelInput.ChannelChanged += OnChannelChanged;
        }

        private void OnDisable()
        {
            volumeInput.VolumeChanged -= OnVolumeChanged;
            channelInput.ChannelChanged -= OnChannelChanged;
        }

        public void OnVolumeChanged(float volume, float minVolume, float maxVolume)
        {
            var actualVolume = volume / maxVolume;
            _audioSource.volume = actualVolume;
        }
        
        public void OnChannelChanged(float channel, float minChannel, float maxChannel)
        {
            var newSongIndex = Mathf.RoundToInt(channel / maxChannel * (songs.Count - 1));

            if (newSongIndex != _currentSongIndex)
            {
                _currentSongIndex = newSongIndex;
                PlayCurrentSong();
            }
        }

        private void ShuffleSongs()
        {
            songs = songs.OrderBy(i => Guid.NewGuid()).ToList();
        }

        private void PlayCurrentSong()
        {
            _audioSource.clip = songs[_currentSongIndex];
            _audioSource.Play();
        }
    }
}