using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Random = UnityEngine.Random;

namespace FrontSide.Radio
{
    [RequireComponent(typeof(AudioSource))]
    public class RadioChannel : MonoBehaviour
    {
        [SerializeField] private List<AssetReference> _audioClips;
        
        private AudioSource _audioSource;
        private AsyncOperationHandle<AudioClip> _currentAudioClipHandle;
        private int _currentAudioClipIndex;
        private bool _isLoadingClip;
        
        public AudioSource AudioSource => _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private async void Start()
        {
            _isLoadingClip = true;
            
            ShuffleAudioClips();
            _currentAudioClipIndex = 0;
            _audioSource.volume = 0f;
            
            var audioClip = await LoadAudioClipAsync(_audioClips[_currentAudioClipIndex]);

            if (audioClip is null)
            {
                Debug.LogWarning($"{nameof(RadioChannel)}: Failed to load first audio clip. May be invalid AssetReference.");
                return;
            }
            
            PlayNewAudioClip(audioClip, Random.Range(0f, audioClip.length));
            
            // для тесту
            // PlayNewAudioClip(audioClip, audioClip.length - 10f);

            _isLoadingClip = false;
        }

        private async void Update()
        {
            if (_audioSource.clip is null || _isLoadingClip) return;
            
            if (_audioSource.isPlaying == false && _audioSource.clip is not null)
            {
                _isLoadingClip = true;

                if (_currentAudioClipIndex >= _audioClips.Count - 1)
                {
                    ShuffleAudioClips();
                }
                
                UnloadCurrentAudioClip();
                SetNextAudioClipIndex();
                
                var newAudioClip = await LoadAudioClipAsync(_audioClips[_currentAudioClipIndex]);
                
                if (newAudioClip is null)
                {
                    Debug.LogWarning($"{nameof(RadioChannel)}: Failed to load audio clip at index {_currentAudioClipIndex}. " +
                                     "May be invalid AssetReference.");
                    return;
                }
                
                PlayNewAudioClip(newAudioClip);
                
                // для тесту
                // PlayNewAudioClip(newAudioClip, newAudioClip.length - 10f);
                
                _isLoadingClip = false;
            }
        }

        private async Task<AudioClip> LoadAudioClipAsync(AssetReference audioClipReference)
        {
            if (_audioClips.Count == 0) return null;
            
            if (_currentAudioClipHandle.IsValid() && _currentAudioClipHandle.Status == AsyncOperationStatus.Succeeded)
            {
                return _currentAudioClipHandle.Result;
            }
            
            var handle = audioClipReference.LoadAssetAsync<AudioClip>();
            
            await handle.Task;
            
            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogWarning($"{nameof(RadioChannel)}: Failed to load audio clip {audioClipReference}");
                return null;
            }
            
            _currentAudioClipHandle = handle;
            
            return handle.Result;
        }
        
        private void UnloadCurrentAudioClip()
        {
            if (_currentAudioClipHandle.IsValid())
            {
                Addressables.Release(_currentAudioClipHandle);
                _currentAudioClipHandle = default;
            }
        }
        
        private void PlayNewAudioClip(AudioClip clip, float time = 0f)
        {
            _audioSource.clip = clip;
            _audioSource.time = time;
            _audioSource.Play();
        }
        
        private void SetNextAudioClipIndex()
        {
            _currentAudioClipIndex = (_currentAudioClipIndex + 1) % _audioClips.Count;
        }

        private void ShuffleAudioClips()
        {
            if (_audioClips.Count < 2) return;

            var lastClip = _audioClips[^1];

            for (var i = _audioClips.Count - 1; i > 0; i--)
            {
                var randomIndex = Random.Range(0, i + 1);
                (_audioClips[i], _audioClips[randomIndex]) = (_audioClips[randomIndex], _audioClips[i]);
            }

            if (_audioClips[0] == lastClip)
            {
                var randomIndex = Random.Range(1, _audioClips.Count);
                (_audioClips[0], _audioClips[randomIndex]) = (_audioClips[randomIndex], _audioClips[0]);
            }
        }
    }
}