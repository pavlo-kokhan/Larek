using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Core.Panels
{
    public class DraggablePanel : MonoBehaviour, IDragHandler
    {
        [Inject] private Canvas _canvas;
        
        private RectTransform _rectTransform;
        private RectTransform _canvasRectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasRectTransform = _canvas.GetComponent<RectTransform>();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
                ClampToScreenBounds();
            }
        }
        
        private void ClampToScreenBounds()
        {
            Vector3[] parentCorners = new Vector3[4];
            _canvasRectTransform.GetWorldCorners(parentCorners);
            Vector3 screenMin = parentCorners[0];
            Vector3 screenMax = parentCorners[2];

            Vector3[] panelCorners = new Vector3[4];
            _rectTransform.GetWorldCorners(panelCorners);
            Vector3 panelMin = panelCorners[0];
            Vector3 panelMax = panelCorners[2];

            Vector3 offset = Vector3.zero;

            if (panelMin.x < screenMin.x) offset.x = screenMin.x - panelMin.x;
            if (panelMax.x > screenMax.x) offset.x = screenMax.x - panelMax.x;
            if (panelMin.y < screenMin.y) offset.y = screenMin.y - panelMin.y;
            if (panelMax.y > screenMax.y) offset.y = screenMax.y - panelMax.y;

            _rectTransform.anchoredPosition += (Vector2)offset / _canvas.scaleFactor;
        }
    }
}