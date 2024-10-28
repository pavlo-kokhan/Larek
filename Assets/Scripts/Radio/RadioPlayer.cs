using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Radio
{
    public class RadioPlayer : MonoBehaviour
    {
        [SerializeField] private RadioVolumeInput volumeInput;
        [SerializeField] private RadioChannelInput channelInput;
        [SerializeField] private List<RadioChannel> channels;

        private AudioSource _audioSource;
        private float _currentVolume;

        private void Start()
        {
            _audioSource = channels.First().AudioSource;
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

        private void OnVolumeChanged(float volume, float minVolume, float maxVolume)
        {
            _currentVolume = volume / maxVolume;
            _audioSource.volume = _currentVolume;
        }
        
        private void OnChannelChanged(float channel, float minChannel, float maxChannel)
        {
            var count = channels.Count;
            
            if (channel <= 10f)
            {
                SwitchAudioSource(0);
            }
            else if (channel > 10 && channel <= 20f && count > 1)
            {
                SwitchAudioSource(1);
            }
            else if (channel > 20 && channel <= 30f && count > 2)
            {
                SwitchAudioSource(2);
            }
            else if (channel > 30 && channel <= 40f && count > 3)
            {
                SwitchAudioSource(3);
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