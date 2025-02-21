using Core;
using Kitchen.Products;
using Kitchen.Products.OnTable;
using UnityEngine;

namespace Kitchen.ChoppingBoard
{
    [RequireComponent(typeof(Collider2D))]
    public class ChoppingBoard : ClickableObjectWithUI
    {
        private ChoppingBoardPanel _panel;
        private Product _product;
        
        protected override void OnPanelLoaded()
        {
            if (_interactivePanelInstance.TryGetComponent<ChoppingBoardPanel>(out var panel) == false)
            {
                Debug.LogWarning($"{nameof(_interactivePanelInstance)} requires {nameof(ChoppingBoardPanel)} component.");
                return;
            }
            
            _panel = panel;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<ProductObject>(out var productObject))
            {
                if (_product is not null) return;
                
                _product = productObject.Product;
                Debug.Log($"{nameof(ChoppingBoard)} product of type {_product.Type} is saved on chopping board.");
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent<ProductObject>(out var productObject))
            {
                if (_product is null) return;

                if (_product == productObject.Product)
                {
                    _product = null;
                    Debug.Log($"{nameof(ChoppingBoard)} product of type {productObject.Product.Type} is deleted from chopping board.");
                }
            }
        }
    }
}