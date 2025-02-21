using UnityEngine;
using UnityEngine.EventSystems;

namespace Core
{
    public class PointerEnterScaler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Vector3 _hoveredScale = new (1.1f, 1.1f, 1.1f);
        [SerializeField] private float _scaleSpeed = 0.1f;

        private Vector3 _idleScale;
        private bool _isHovered;

        private void Start()
        {
            _idleScale = transform.localScale;
        }

        private void Update()
        {
            if (_isHovered)
            {
                transform.localScale = Vector3.Lerp(
                    transform.localScale, 
                    _hoveredScale, 
                    _scaleSpeed);
            }
            else
            {
                transform.localScale = Vector3.Lerp(
                    transform.localScale, 
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