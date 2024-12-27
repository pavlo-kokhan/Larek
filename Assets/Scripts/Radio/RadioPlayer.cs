using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Radio
{
    [RequireComponent(typeof(AudioSource))]
    public class RadioPlayer : MonoBehaviour
    {
        [SerializeField] private List<RadioChannel> _channels;
        [SerializeField] private RadioChannel _noSignalChannel;
        [SerializeField] private AudioClip _noSignalAudioClip;
        [SerializeField] [Range(0.05f, 0.1f)] private float _channelPositionLength = 0.1f;
        [SerializeField] [Range(0.05f, 0.1f)] private float _channelEdgePositionsOffset = 0.05f;
        [SerializeField] [Range(0f, 0.05f)] private float _valuesChangingStep = 0.01f;

        private readonly Dictionary<RadioChannel, Vector2> _channelPositions = new ();
        
        private RadioSliderInput _volumeInput;
        private RadioSliderInput _channelInput;
        private AudioSource _audioSource;
        private RadioChannel _currentChannel;
        private bool _isNoSignal;
        private bool _isInitialized;

        private void Start()
        {
            InitializeChannelPositions();
            _audioSource = GetComponent<AudioSource>();
            _audioSource.volume = 0f;
            PlayNoSignalAudioClip();
        }
        
        public void Initialize(RadioSliderInput volumeInput, RadioSliderInput channelInput)
        {
            if (_isInitialized) return;
            
            _volumeInput = volumeInput;
            _channelInput = channelInput;

            _volumeInput.SliderValueChanged += OnVolumeChanged;
            _channelInput.SliderValueChanged += OnChannelChanged;
            
            foreach (var channel in _channels)
            {
                channel.AudioClipSwitched += OnChannelAudioClipSwitched;
            }
            
            _isInitialized = true;
        }
        
        private void OnDestroy()
        {
            _volumeInput.SliderValueChanged -= OnVolumeChanged;
            _channelInput.SliderValueChanged -= OnChannelChanged;
            
            foreach (var channel in _channels)
            {
                channel.AudioClipSwitched -= OnChannelAudioClipSwitched;
            }
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
            _channelPositions.Clear();
            
            foreach (var channel in _channels)
            {
                var start = currentStart;
                var end = start + _channelPositionLength;
                
                _channelPositions.Add(channel, new Vector2(start, end));

                currentStart = end + gap;
            }
            
            foreach (var channelPosition in _channelPositions)
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

            var selectedPosition = _channelPositions
                .FirstOrDefault(p => newValue > p.Value.x && newValue < p.Value.y);
            
            if (EqualityComparer<KeyValuePair<RadioChannel, Vector2>>.Default.Equals(selectedPosition, default))
            {
                PlayNoSignalAudioClip();
                Debug.Log($"Channel changed to {newValue}");
                return;
            }
            
            SwitchChannel(selectedPosition.Key);
            Debug.Log($"Channel changed to {newValue}");
        }

        private void OnChannelAudioClipSwitched(RadioChannel channel)
        {
            if (!_isInitialized || _currentChannel != channel) return;

            PlayNewAudioClip(_currentChannel.CurrentAudioClip, _currentChannel.CurrentAudioClipTime);
        }

        private void SwitchChannel(RadioChannel channel)
        {
            if (_currentChannel == channel) return;
            
            _currentChannel = channel;
            PlayNewAudioClip(_currentChannel.CurrentAudioClip, 
                _currentChannel.CurrentAudioClipTime);
        }

        private void PlayNewAudioClip(AudioClip clip, float time = 0f)
        {
            _isNoSignal = false;
            _audioSource.loop = false;
            _audioSource.clip = clip;
            _audioSource.time = time;
            _audioSource.Play();
        }

        private void PlayNoSignalAudioClip()
        {
            if (_isNoSignal) return;
            
            _isNoSignal = true;
            _audioSource.clip = _noSignalAudioClip;
            _audioSource.loop = true;
            _audioSource.Play();
        }
        
        private void SetVolume(float volume)
        {
            _audioSource.volume = volume; // todo: multiply on settings music volume
        }
    }
}