using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Random = UnityEngine.Random;

namespace Radio
{
    [RequireComponent(typeof(AudioSource))]
    public class RadioChannel : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private List<AssetReference> _audioClips;
        
        private readonly Dictionary<AssetReference, AudioClip> _audioClipsCache = new();
        private readonly Dictionary<AudioClip, AsyncOperationHandle> _loadOperationsCache = new();
        
        private int _currentAudioClipIndex;
        
        public AudioSource AudioSource => _audioSource;
        
        private async void Start()
        {
            ShuffleAudioClips();
            _currentAudioClipIndex = 0;
            _audioSource.volume = 0f;
            await PlayCurrentAudioClip();
        }

        private async void Update()
        {
            if (_audioSource.clip != null && _audioSource.time >= _audioSource.clip.length - 0.1f)
            {
                var currentClipAsset = _audioClips[_currentAudioClipIndex];
                var clip = _audioClipsCache[currentClipAsset];
                
                // todo release handle and clear ram
                // _loadOperationsCache[clip].Release();
                // _audioClips.Remove(currentClipAsset);
                // _loadOperationsCache.Remove(clip);
                
                SetNextAudioClipIndex();
                await PlayCurrentAudioClip();
            }
        }

        private async Task PlayCurrentAudioClip()
        {
            if (_audioClips.Count == 0) return;

            var clipReference = _audioClips[_currentAudioClipIndex];

            var handle = clipReference.LoadAssetAsync<AudioClip>();
            await handle.Task;

            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogError($"Failed to load audio clip {clipReference}");
                return;
            }
            
            var clip = handle.Result;
            _audioClipsCache[clipReference] = clip;
            _loadOperationsCache[clip] = handle;

            var clipStartTime = _currentAudioClipIndex == 0 ? Random.Range(0f, clip.length) : 0f;
            PlayNewAudioClip(clip, clipStartTime);
        }

        private void PlayNewAudioClip(AudioClip clip, float time = 0f)
        {
            _audioSource.clip = clip;
            _audioSource.time = time;
            _audioSource.Play();
        }
        
        private void SetNextAudioClipIndex()
        {
            if (_currentAudioClipIndex < _audioClips.Count - 2)
            {
                _currentAudioClipIndex++;
                return;
            }
            
            ShuffleAudioClips();
            _currentAudioClipIndex = 0;
        }

        private void ShuffleAudioClips()
        {
            if (_audioClips.Count < 2) return;
            
            var random = new System.Random();
            _audioClips = _audioClips.OrderBy(x => random.Next()).ToList();
        }
    }
}