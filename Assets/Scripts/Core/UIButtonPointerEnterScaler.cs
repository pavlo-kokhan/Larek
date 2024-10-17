using UnityEngine;
using UnityEngine.EventSystems;

namespace Core
{
    public class UIButtonPointerEnterScaler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Vector3 hoveredScale;
        [SerializeField] private float scaleSpeed;

        private RectTransform _rectTransform;
        private Vector3 _idleScale;
        private bool _isHovered;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _idleScale = _rectTransform.localScale;
        }

        private void Update()
        {
            if (_isHovered)
            {
                _rectTransform.localScale = Vector3.Lerp(
                    _rectTransform.localScale, 
                    hoveredScale, 
                    scaleSpeed);
            }
            else
            {
                _rectTransform.localScale = Vector3.Lerp(
                    _rectTransform.localScale, 
                    _idleScale, 
                    scaleSpeed);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _isHovered = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isHovered = false;
        }
    }
}