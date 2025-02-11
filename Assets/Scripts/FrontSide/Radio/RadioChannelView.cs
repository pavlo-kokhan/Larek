using UnityEngine;

namespace FrontSide.Radio
{
    public class RadioChannelView : MonoBehaviour
    {
        [SerializeField] private RadioSliderInput _sliderInput;
        [SerializeField] RectTransform _cursorRectTransform;
        [SerializeField] private float _maxCursorX = 445f;
        
        private float _minCursorX;

        private void OnEnable()
        {
            _sliderInput.SliderValueChanged += OnChannelChanged;
        }

        private void OnDisable()
        {
            _sliderInput.SliderValueChanged -= OnChannelChanged;
        }

        private void Start()
        {
            _minCursorX = _cursorRectTransform.anchoredPosition.x;
        }

        private void OnChannelChanged(float newValue, float minValue, float maxValue)
        {
            float newCursorX = Mathf.Lerp(_minCursorX, _maxCursorX, newValue / maxValue);
            _cursorRectTransform.anchoredPosition = new Vector2(newCursorX, _cursorRectTransform.anchoredPosition.y);
        }
    }
}
