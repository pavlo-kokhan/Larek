using System;
using System.Collections.Generic;
using UnityEngine;

namespace Radio
{
    public class RadioChannelInput : MonoBehaviour
    {
        public event Action<float, float, float> ChannelChanged;
        
        [SerializeField] private float channelStep;
        
        private readonly float _minSliderValue = 0f;
        private readonly float _maxSliderValue = 100f;
        
        private float _currentChannel;

        private void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                ChangeChannel(channelStep);
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                ChangeChannel(-channelStep);
            }
        }

        private void ChangeChannel(float change)
        {
            _currentChannel = Mathf.Clamp(_currentChannel + change, _minSliderValue, _maxSliderValue);
            ChannelChanged?.Invoke(_currentChannel, _minSliderValue, _maxSliderValue);
            
            Debug.Log($"Channel: {_currentChannel}");
        }
    }
}