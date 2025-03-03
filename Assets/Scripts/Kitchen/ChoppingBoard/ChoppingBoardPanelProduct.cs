using Kitchen.Products.ProductGameObject;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Kitchen.ChoppingBoard
{
    [RequireComponent(typeof(Collider2D))]
    public class ChoppingBoardPanelProduct : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private ChoppingBoardPanel _choppingBoardPanel;
        [SerializeField] private Image _productImage;
        
        private void OnEnable()
        {
            var product = _choppingBoardPanel.ProductObject.Product;
            
            if (product is null)
            {
                _productImage.sprite = null;
                _productImage.color = new Color(0, 0, 0, 0);
            }
            else
            {
                _productImage.sprite = product.OnChoppingBoardPanelSprite;
                _productImage.color = new Color(255, 255, 255, 255);
            }
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                OnLeftButtonClicked();
            }
        }

        private void OnLeftButtonClicked()
        {
            var productObject = _choppingBoardPanel.ProductObject;

            if (productObject is null) return;

            if (!productObject.TryGetComponent<ProductChoppingBehaviour>(out var choppingBehaviour))
            {
                Debug.LogWarning($"{nameof(productObject)} has no component of type {nameof(ProductChoppingBehaviour)}");
                return;
            }
            
            choppingBehaviour.UpdateChoppingStage();
        }
    }
}