using UnityEngine;

namespace Radio
{
    public class RadioChannelView : MonoBehaviour
    {
        [SerializeField] private RadioSliderInput sliderInput;
        [SerializeField] RectTransform cursorRectTransform;
        [SerializeField] private float maxCursorX = 445f;
        
        private float _minCursorX;

        private void OnEnable()
        {
            sliderInput.SliderValueChanged += OnChannelChanged;
        }

        private void OnDisable()
        {
            sliderInput.SliderValueChanged -= OnChannelChanged;
        }

        private void Start()
        {
            _minCursorX = cursorRectTransform.anchoredPosition.x;
        }

        private void OnChannelChanged(float newValue, float minValue, float maxValue)
        {
            float newCursorX = Mathf.Lerp(_minCursorX, maxCursorX, newValue / maxValue);
            cursorRectTransform.anchoredPosition = new Vector2(newCursorX, cursorRectTransform.anchoredPosition.y);
        }
    }
}
