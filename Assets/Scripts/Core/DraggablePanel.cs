using UnityEngine;
using UnityEngine.EventSystems;

namespace Core
{
    public class DraggablePanel : MonoBehaviour, IDragHandler
    {
        [SerializeField] private Canvas canvas;
        
        private RectTransform _rectTransform;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }
}