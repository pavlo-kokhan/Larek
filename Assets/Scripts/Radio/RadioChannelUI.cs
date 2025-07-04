using UnityEngine;

namespace Radio
{
    public class RadioChannelUI : MonoBehaviour
    {
        [SerializeField] private RectTransform cursorRectTransform;
        
        private RectTransform _selectorRectTransform;
        private float _minRotationZ = 330f;
        private float _maxRotationZ = 30f;
        private float _minCursorX;
        private float _maxCursorX = 410f;

        private void Awake()
        {
            _selectorRectTransform = GetComponent<RectTransform>();
            _minCursorX = cursorRectTransform.anchoredPosition.x;
        }

        public void OnChannelChanged(float newChannel, float minChannel, float maxChannel)
        {
            float rotationZ = Mathf.Lerp(_minRotationZ, _maxRotationZ, newChannel / maxChannel);
            _selectorRectTransform.localRotation = Quaternion.Euler(0, 0, rotationZ);
            
            float newCursorX = Mathf.Lerp(_minCursorX, _maxCursorX, newChannel / maxChannel);
            cursorRectTransform.anchoredPosition = new Vector2(newCursorX, cursorRectTransform.anchoredPosition.y);
        }
    }
}
