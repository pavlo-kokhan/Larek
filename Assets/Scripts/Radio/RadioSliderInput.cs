using System;
using UnityEngine;

namespace Radio
{
    public class RadioSliderInput : MonoBehaviour
    {
        private const float SliderMinValue = 0f;
        private const float SliderMaxValue = 1f;
        
        public event Action<float, float, float> SliderValueChanged;
        
        [SerializeField] private RectTransform sliderRectTransform;
        [SerializeField] private float minAngle = 20f;
        [SerializeField] private float maxAngle = 340f;
        
        private float _currentAngle;
        private float _lastAngle;
        private float _sliderFraction;
        
        private void Start()
        {
            _currentAngle = minAngle;
            _lastAngle = minAngle;
            _sliderFraction = SliderMinValue;
        }

        public void OnBeginDrag()
        {
            Vector2 direction = (Vector2)Input.mousePosition - (Vector2)sliderRectTransform.position;
            _lastAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _currentAngle = sliderRectTransform.eulerAngles.z;
        }

        public void OnDrag()
        {
            Vector2 direction = (Vector2)Input.mousePosition - (Vector2)sliderRectTransform.position;
            float newAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            float angleDifference = Mathf.DeltaAngle(_lastAngle, newAngle);
            _currentAngle = Mathf.Clamp(_currentAngle + angleDifference, minAngle, maxAngle);
            sliderRectTransform.rotation = Quaternion.Euler(0, 0, _currentAngle);
            _lastAngle = newAngle;
            
            _sliderFraction = Mathf.InverseLerp(maxAngle, minAngle, _currentAngle);
            Debug.Log(_sliderFraction);
            
            SliderValueChanged?.Invoke(_sliderFraction, SliderMinValue, SliderMaxValue);
        }
    }
}