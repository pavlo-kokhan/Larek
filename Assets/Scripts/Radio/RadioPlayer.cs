using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;

namespace Radio
{
    public class RadioPlayer : MonoBehaviour
    {
        [SerializeField] private List<RadioChannel> _channels;

        private RadioSliderInput _volumeInput;
        private RadioSliderInput _channelInput;
        private AudioSource _currentAudioSource;
        private float _currentVolume;
        private bool _isInitialized;

        public void Initialize(RadioSliderInput volumeInput, RadioSliderInput channelInput)
        {
            if (_isInitialized) return;
            
            _volumeInput = volumeInput;
            _channelInput = channelInput;
            
            _volumeInput.SliderValueChanged += OnVolumeChanged;
            _channelInput.SliderValueChanged += OnChannelChanged;
            
            _currentAudioSource = _channels.Last().AudioSource;
            
            _isInitialized = true;
        }
        
        private void OnDestroy()
        {
            _volumeInput.SliderValueChanged -= OnVolumeChanged;
            _channelInput.SliderValueChanged -= OnChannelChanged;
        }

        private void OnVolumeChanged(float newValue, float minValue, float maxValue)
        {
            if (!_isInitialized) return;
            
            _currentVolume = newValue / maxValue;
            _currentAudioSource.volume = _currentVolume;
            Debug.Log($"Volume changed to {newValue}");
        }
        
        private void OnChannelChanged(float newValue, float minValue, float maxValue)
        {
            if (!_isInitialized) return;
            
            var count = _channels.Count;
            var channel = newValue / maxValue;
            
            // todo
            if (channel >= 0.1f && channel <= 0.15f)
            {
                SwitchAudioSource(0);
            }
            else if (channel >= 0.3f && channel <= 0.35f && count > 1)
            {
                SwitchAudioSource(1);
            }
            else if (channel >= 0.6f && channel <= 0.65f && count > 2)
            {
                SwitchAudioSource(2);
            }
            else
            {
                SwitchAudioSource(_channels.Count - 1);
            }
            Debug.Log($"Channel changed to {newValue}");
        }

        private void SwitchAudioSource(int index)
        {
            _currentAudioSource.volume = 0f;
            _currentAudioSource = _channels[index].AudioSource;
            _currentAudioSource.volume = _currentVolume;
        }
    }
}