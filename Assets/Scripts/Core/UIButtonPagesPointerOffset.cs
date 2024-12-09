using UnityEngine;
using UnityEngine.EventSystems;

namespace Core
{
    public class UIButtonPagesPointerOffset  : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Vector3 _hoveredPosition;
        [SerializeField] private float _offsetSpeed = 0.1f;

        private RectTransform _rectTransform;
        private Vector3 _idlePosition;
        private bool _isHovered;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _idlePosition = _rectTransform.localPosition;
        }
        
        private void Update()
        {
            if (_isHovered)
            {
                _rectTransform.localPosition = Vector3.Lerp(
                    _rectTransform.localPosition, 
                    _hoveredPosition, 
                    _offsetSpeed);
            }
            else
            {
                _rectTransform.localPosition = Vector3.Lerp(
                    _rectTransform.localPosition, 
                    _idlePosition, 
                    _offsetSpeed);
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