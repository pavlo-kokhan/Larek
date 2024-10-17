using UnityEngine;
using UnityEngine.EventSystems;

namespace Notebook
{
    public class UIButtonPagesPointerOffset  : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Vector3 hoveredPosition;
        [SerializeField] private float offsetSpeed;

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
                    hoveredPosition, 
                    offsetSpeed);
            }
            else
            {
                _rectTransform.localPosition = Vector3.Lerp(
                    _rectTransform.localPosition, 
                    _idlePosition, 
                    offsetSpeed);
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