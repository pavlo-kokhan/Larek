using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Panels
{
    [RequireComponent(typeof(RectTransform))]
    public class ClosablePanel : MonoBehaviour, IPointerClickHandler
    {
        [Inject] private InteractivePanelsRegistrator _panelsRegistrator;
        
        private RectTransform _rectTransform;
        private Vector3 _startPosition;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _startPosition = _rectTransform.anchoredPosition;
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                OnRightButtonClicked();
            }
        }
        
        private void OnRightButtonClicked()
        {
            _panelsRegistrator.UnregisterPanel(gameObject);
            _rectTransform.anchoredPosition = _startPosition;
        }
    }
}