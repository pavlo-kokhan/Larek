using Cursors;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Kitchen.TrashContainer
{
    public class TrashContainer : MonoBehaviour, IPointerClickHandler
    {
        [Inject] private CursorView _cursorView;
        [Inject] private ProductHolder _productHolder;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                _cursorView.ClearCursorProductIcon();
                _productHolder.DeleteHoldingProduct();
            }
        }
    }
}