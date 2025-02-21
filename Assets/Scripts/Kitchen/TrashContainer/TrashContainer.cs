using Kitchen.Products;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Kitchen.TrashContainer
{
    public class TrashContainer : MonoBehaviour, IPointerClickHandler
    {
        private ProductHolder _productHolder;
        private IAcceptProductCondition _acceptCondition;
        
        [Inject]
        public void Construct(ProductHolder productHolder)
        {
            _productHolder = productHolder;
            _acceptCondition = new TrashContainerAcceptCondition();
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                _productHolder.TryReturnProduct(_acceptCondition, out _);
            }
        }
    }
}