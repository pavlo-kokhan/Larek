using UnityEngine;

namespace Core
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [SerializeField] private AudioSource _soundEffectsSource;

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

        public void PlaySfx(AudioClip clip)
        {
            _soundEffectsSource.PlayOneShot(clip);
        }

        public void SetSfxVolume(float volume)
        {
            _soundEffectsSource.volume = volume;
        }
    }
}