using Core.Cursors;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Kitchen.TrashContainer
{
    public class TrashContainer : MonoBehaviour, IPointerClickHandler
    {
        private ProductHolder _productHolder;
        
        [Inject]
        public void Construct(ProductHolder productHolder)
        {
            _productHolder = productHolder;
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                _productHolder.TryReturnProduct(_ => true, out _);
            }
        }
    }
}