using UnityEngine;
using UnityEngine.EventSystems;

namespace Core
{
    public class ClosablePanel : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private ClickableObject clickableObject;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                clickableObject.ClosePanel();
            }
        }
    }
}