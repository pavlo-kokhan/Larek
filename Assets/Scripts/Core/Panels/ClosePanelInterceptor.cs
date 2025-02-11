using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.Panels
{
    public class ClosePanelInterceptor : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private ClosablePanel _closablePanel;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                _closablePanel.ClosePanel();
            }
        }
    }
}