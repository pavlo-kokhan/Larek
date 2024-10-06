using System;
using UnityEngine;

namespace Radio
{
    public class VolumeInputController : MonoBehaviour
    {
        public event Action<float, float, float> VolumeChangedUI;
        public event Action<float> VolumeChangedSong;
        
        [SerializeField] private float volume;
        [SerializeField] private float volumeStep;
        
        private readonly float _minSliderValue = 0f;
        private readonly float _maxSliderValue = 100f;

        private void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                ChangeVolume(volumeStep);
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                ChangeVolume(-volumeStep);
            }
        }
        
        private void ChangeVolume(float changeValue)
        {
            volume = Mathf.Clamp(volume + changeValue, _minSliderValue, _maxSliderValue);
            VolumeChangedUI?.Invoke(volume, _minSliderValue, _maxSliderValue);
            VolumeChangedSong?.Invoke(volume);
            
            Debug.Log($"Volume: {volume}");
        }
    }
}