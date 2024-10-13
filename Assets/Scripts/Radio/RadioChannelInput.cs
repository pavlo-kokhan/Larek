using System;
using System.Collections.Generic;
using UnityEngine;

namespace Radio
{
    public class RadioChannelInput : MonoBehaviour
    {
        public event Action<float, float, float> ChannelChangedUI;
        public event Action<AudioClip> ChannelChangedClip;
        
        [SerializeField] private float channelStep;
        
        [SerializeField] private List<AudioClip> songs;
        
        private readonly float _minSliderValue = 0f;
        private readonly float _maxSliderValue = 100f;
        
        private float _channelSwitchStep;
        private int _currentSongIndex;
        private bool _isPlaying;
        private float _channel = 0f;

        private void Start()
        {
            if (songs.Count > 0)
            {
                _channelSwitchStep = _maxSliderValue / songs.Count;
                _currentSongIndex = Mathf.RoundToInt(_channel / _channelSwitchStep);
                _currentSongIndex = Mathf.Clamp(_currentSongIndex, 0, songs.Count - 1);
                
                // todo КОСТИЛЬ
                PlayFirstSong();
            }
            else
            {
                Debug.LogWarning("No song available in the radio");
            }
        }

        private void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                ChangeChannel(channelStep);
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                ChangeChannel(-channelStep);
            }
        }

        private void ChangeChannel(float change)
        {
            _channel = Mathf.Clamp(_channel + change, _minSliderValue, _maxSliderValue);
            ChannelChangedUI?.Invoke(_channel, _minSliderValue, _maxSliderValue);
            PlayNewSongOrContinue();
            
            Debug.Log($"Channel: {_channel}");
        }
        
        private void PlayNewSongOrContinue()
        {
            if (songs.Count == 0) return;
            
            int newChannelIndex = Mathf.RoundToInt(_channel / _channelSwitchStep);
            newChannelIndex = Mathf.Clamp(newChannelIndex, 0, songs.Count - 1);

            if (!_isPlaying || _currentSongIndex != newChannelIndex)
            {
                _currentSongIndex = newChannelIndex;
                ChannelChangedClip?.Invoke(songs[_currentSongIndex]);
                _isPlaying = true;
            }
        }
        
        private void PlayFirstSong()
        {
            _currentSongIndex = Mathf.Clamp(0, 0, songs.Count - 1);
            ChannelChangedClip?.Invoke(songs[_currentSongIndex]);
            _isPlaying = true;
        }
    }
}