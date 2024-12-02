using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Radio
{
    public class RadioPlayer : MonoBehaviour
    {
        [SerializeField] RadioSliderInput volumeInput;
        [SerializeField] private RadioSliderInput channelInput;
        [SerializeField] private List<RadioChannel> channels;

        private AudioSource _audioSource;
        private float _currentVolume;

        private void Start()
        {
            _audioSource = channels.Last().AudioSource;
        }

        private void OnEnable()
        {
            volumeInput.SliderValueChanged += OnVolumeChanged;
            channelInput.SliderValueChanged += OnChannelChanged;
        }

        private void OnDisable()
        {
            volumeInput.SliderValueChanged -= OnVolumeChanged;
            channelInput.SliderValueChanged -= OnChannelChanged;
        }

        private void OnVolumeChanged(float newValue, float minValue, float maxValue)
        {
            _currentVolume = newValue / maxValue;
            _audioSource.volume = _currentVolume;
        }
        
        private void OnChannelChanged(float newValue, float minValue, float maxValue)
        {
            var count = channels.Count;
            var channel = newValue / maxValue;
            
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
                SwitchAudioSource(channels.Count - 1);
            }
        }

        private void SwitchAudioSource(int index)
        {
            _audioSource.volume = 0f;
            _audioSource = channels[index].AudioSource;
            _audioSource.volume = _currentVolume;
        }
    }
}