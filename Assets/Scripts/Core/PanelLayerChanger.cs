using UnityEngine;
using UnityEngine.EventSystems;

namespace Core
{
    public class PanelLayerChanger : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private RectTransform _uiContainerTransform;
        [SerializeField] private ClickableObjectWithUI _clickableObjectWithUI;

        private void OnEnable()
        {
            _clickableObjectWithUI.PanelOpened += SetAsLastSibling;
        }

        private void OnDisable()
        {
            _clickableObjectWithUI.PanelOpened -= SetAsLastSibling;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                SetAsLastSibling();
            }
        }

        private void SetAsLastSibling()
        {
            _uiContainerTransform.SetAsLastSibling();
        }
    }
}