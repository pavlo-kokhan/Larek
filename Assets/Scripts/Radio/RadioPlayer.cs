using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Radio
{
    public class RadioPlayer : MonoBehaviour
    {
        [SerializeField] private List<RadioChannel> _channels;
        [SerializeField] private AudioClip _noSignalAudioClip;
        [SerializeField] private AudioSource _noSignalAudioSource;
        [SerializeField] [Range(0.05f, 0.1f)] private float _channelPositionLength = 0.1f;
        [SerializeField] [Range(0.05f, 0.1f)] private float _channelEdgePositionsOffset = 0.05f;

        private readonly Dictionary<RadioChannel, Vector2> _channelsPositions = new ();
        
        private RadioSliderInput _volumeInput;
        private RadioSliderInput _channelInput;
        private AudioSource _currentChannelAudioSource;
        private float _currentVolume;
        private bool _isInitialized;

        public void Initialize(RadioSliderInput volumeInput, RadioSliderInput channelInput)
        {
            if (_isInitialized) return;
            
            _volumeInput = volumeInput;
            _channelInput = channelInput;

            _volumeInput.SliderValueChanged += OnVolumeChanged;
            _channelInput.SliderValueChanged += OnChannelChanged;
            
            InitializeChannelPositions();
            _noSignalAudioSource.clip = _noSignalAudioClip;
            _noSignalAudioSource.loop = true;
            _noSignalAudioSource.volume = 0f;
            _currentChannelAudioSource = _noSignalAudioSource;
            _currentChannelAudioSource.Play();
            
            _isInitialized = true;
        }

        private void OnDestroy()
        {
            _volumeInput.SliderValueChanged -= OnVolumeChanged;
            _channelInput.SliderValueChanged -= OnChannelChanged;
        }

        private void InitializeChannelPositions()
        {
            if (_channels == null || _channels.Count == 0)
            {
                Debug.LogError("Channel list is empty. Cannot calculate positions.");
                return;
            }
            
            var channelsCount = _channels.Count;
            var totalChannelLength = channelsCount * _channelPositionLength;
            var remainingSpace = 1f - totalChannelLength - 2 * _channelEdgePositionsOffset;

            if (remainingSpace < 0f)
            {
                Debug.LogError("Channel list is too long. No space to set channel positions.");
                return;
            }
            
            var gap = channelsCount > 1 ? remainingSpace / (channelsCount - 1) : 0f;
            
            var currentStart = _channelEdgePositionsOffset;
            _channelsPositions.Clear();
            
            foreach (var channel in _channels)
            {
                var start = currentStart;
                var end = start + _channelPositionLength;
                
                _channelsPositions.Add(channel, new Vector2(start, end));

                currentStart = end + gap;
            }
            
            foreach (var channelPosition in _channelsPositions)
            {
                Debug.Log($"({channelPosition.Value.x}, {channelPosition.Value.y})");
            }
        }

        private void OnVolumeChanged(float newValue, float minValue, float maxValue)
        {
            if (!_isInitialized) return;
            
            SetVolume(newValue);
            Debug.Log($"Volume changed to {newValue}");
        }
        
        private void OnChannelChanged(float newValue, float minValue, float maxValue)
        {
            if (!_isInitialized) return;
            
            Debug.Log($"Channel changed to {newValue}");

            var channel = GetChannelOnPosition(newValue);
            
            if (channel != null)
            {
                SetNewAudioSource(channel.AudioSource);
                return;
            }
            
            SetNewAudioSource(_noSignalAudioSource);
        }

        private RadioChannel GetChannelOnPosition(float sliderValue)
        {
            foreach (var channelsPosition in _channelsPositions)
            {
                var range = channelsPosition.Value;
                
                if (sliderValue > range.x && sliderValue < range.y)
                {
                    return channelsPosition.Key;
                }
            }

            return null;
        }

        private void SetNewAudioSource(AudioSource audioSource)
        {
            if (_currentChannelAudioSource == audioSource) return;

            _currentChannelAudioSource.volume = 0f;
            _currentChannelAudioSource = audioSource;
            _currentChannelAudioSource.volume = _currentVolume;
        }

        private void SetVolume(float volume)
        {
            _currentVolume = volume;
            _currentChannelAudioSource.volume = volume;
        }
    }
}