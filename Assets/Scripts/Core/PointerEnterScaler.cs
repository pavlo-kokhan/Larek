using UnityEngine;
using UnityEngine.EventSystems;

namespace Core
{
    [RequireComponent(typeof(RectTransform))]
    public class PointerEnterScaler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Vector3 _hoveredScale = new (1.1f, 1.1f, 1.1f);
        [SerializeField] private float _scaleSpeed = 0.1f;

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
                    _hoveredScale, 
                    _scaleSpeed);
            }
            else
            {
                _rectTransform.localScale = Vector3.Lerp(
                    _rectTransform.localScale, 
                    _idleScale, 
                    _scaleSpeed);
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