using UnityEngine;

namespace Core
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;

        private AudioClip _currentMusicClip;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void PlayMusic(AudioClip clip)
        {
            // if (_currentMusicClip != clip)
            // {
            //     _currentMusicClip = clip;
            //     musicSource.clip = _currentMusicClip;
            //     musicSource.Play();
            // }
            
            _currentMusicClip = clip;
            musicSource.clip = _currentMusicClip;
            musicSource.Play();
        }

        public void PlaySfx(AudioClip clip)
        {
            sfxSource.PlayOneShot(clip);
        }

        public void SetMusicVolume(float volume)
        {
            musicSource.volume = volume / 100f;
        }

        public void SetSfxVolume(float volume)
        {
            sfxSource.volume = volume;
        }
    }
}