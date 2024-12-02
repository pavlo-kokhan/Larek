using UnityEngine;
using UnityEngine.EventSystems;

namespace Core
{
    public class PanelLayerChanger : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] RectTransform uiContainerTransform;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                uiContainerTransform.SetAsLastSibling();
            }
        }
    }
}