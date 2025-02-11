using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.Panels
{
    public class PanelLayerChanger : MonoBehaviour, IPointerDownHandler
    {
        private RectTransform _parentRectTransform;

        private void OnEnable()
        {
            _parentRectTransform.SetAsLastSibling();
        }

        private void Awake()
        {
            _parentRectTransform = GetComponentInParent<RectTransform>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                _parentRectTransform.SetAsLastSibling();
            }
        }
    }
}