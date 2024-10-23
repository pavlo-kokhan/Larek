using UnityEngine;
using UnityEngine.EventSystems;

namespace Core
{
    public class ClosablePanel : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private ClickableObjectWithUI clickableObjectWithUI;
        
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
                clickableObjectWithUI.ClosePanel();
                _rectTransform.anchoredPosition = _startPosition;
            }
        }
    }
}