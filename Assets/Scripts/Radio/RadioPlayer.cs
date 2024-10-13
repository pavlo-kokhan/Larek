using System;
using Core;
using UnityEngine;

namespace Radio
{
    public class RadioPlayer : MonoBehaviour
    {
        [SerializeField] private RadioVolumeInput volumeInput;
        [SerializeField] private RadioChannelInput channelInput;

        private void Start()
        {
           volumeInput.VolumeChanged += OnVolumeChanged;
           channelInput.ChannelChangedClip += OnChannelChangedClip;
        }

        public void OnVolumeChanged(float volume)
        {
            AudioManager.Instance.SetMusicVolume(volume);
        }
        
        public void OnChannelChangedClip(AudioClip clip)
        {
            AudioManager.Instance.PlayMusic(clip);
        }
    }
}