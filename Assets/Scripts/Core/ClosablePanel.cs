using UnityEngine;
using UnityEngine.EventSystems;

namespace Core
{
    public class ClosablePanel : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private ClickableObject clickableObject;
        
        private RectTransform _rectTransform;
        private Vector3 _startPosition;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _startPosition = _rectTransform.anchoredPosition;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                clickableObject.ClosePanel();
                _rectTransform.anchoredPosition = _startPosition;
            }
        }
    }
}