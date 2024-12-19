using UnityEngine;

namespace GlobalAudio
{
    public class GlobalAudioPlayer
    {
        private readonly AudioSource _audioSource;

        public float Volume
        {
            get => _audioSource.volume;
            set => _audioSource.volume = value;
        }

        public GlobalAudioPlayer(AudioSource audioSource)
        {
            _audioSource = audioSource;
        }

        public void PlaySoundEffect(AudioClip clip)
        {
            _audioSource.PlayOneShot(clip);
        }
    }
}