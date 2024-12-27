using System;
using UnityEngine;

namespace Radio
{
    public class RadioSliderInput : MonoBehaviour
    {
        private const float SliderMinValue = 0f;
        private const float SliderMaxValue = 1f;
        
        public event Action<float, float, float> SliderValueChanged;
        
        [SerializeField] private RectTransform _sliderRectTransform;
        [SerializeField] private float _minAngle = 20f;
        [SerializeField] private float _maxAngle = 340f;
        [SerializeField] [Range(0.01f, 0.5f)] private float _debounceInterval = 0.05f;
        [SerializeField] private RadioSliderType _type;
        public RadioSliderType Type => _type;
        
        private float _currentAngle;
        private float _lastAngle;
        private float _sliderFraction;
        private float _lastDebounceTime;
        
        private void Start()
        {
            _currentAngle = _minAngle;
            _lastAngle = _minAngle;
            _sliderFraction = SliderMinValue;
        }

        public void OnBeginDrag()
        {
            var direction = (Vector2)Input.mousePosition - (Vector2)_sliderRectTransform.position;
            _lastAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _currentAngle = _sliderRectTransform.eulerAngles.z;
        }

        public void OnDrag()
        {
            Vector2 direction = (Vector2)Input.mousePosition - (Vector2)_sliderRectTransform.position;
            var newAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            var angleDifference = Mathf.DeltaAngle(_lastAngle, newAngle);
            _currentAngle = Mathf.Clamp(_currentAngle + angleDifference, _minAngle, _maxAngle);
            _sliderRectTransform.rotation = Quaternion.Euler(0, 0, _currentAngle);
            _lastAngle = newAngle;
            
            var newSliderFraction = Mathf.InverseLerp(_maxAngle, _minAngle, _currentAngle);

            if (Time.time - _lastDebounceTime >= _debounceInterval)
            {
                _sliderFraction = newSliderFraction;
                SliderValueChanged?.Invoke(_sliderFraction, SliderMinValue, SliderMaxValue);
                _lastDebounceTime = Time.time;
            }
        }
        
        public void OnEndDrag()
        {
            _sliderFraction = Mathf.InverseLerp(_maxAngle, _minAngle, _currentAngle);
            SliderValueChanged?.Invoke(_sliderFraction, SliderMinValue, SliderMaxValue);
        }
    }
}