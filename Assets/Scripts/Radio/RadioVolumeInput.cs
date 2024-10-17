using System;
using UnityEngine;

namespace Radio
{
    public class RadioVolumeInput : MonoBehaviour
    {
        public event Action<float, float, float> VolumeChanged;
        
        [SerializeField] private float volumeStep;
        
        private readonly float _minSliderValue = 0f;
        private readonly float _maxSliderValue = 100f;
        
        private float _volume;

        private void Start()
        {
            ChangeVolume(0f);
        }

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
            _volume = Mathf.Clamp(_volume + changeValue, _minSliderValue, _maxSliderValue);
            VolumeChanged?.Invoke(_volume, _minSliderValue, _maxSliderValue);
            
            Debug.Log($"Volume: {_volume}");
        }
    }
}