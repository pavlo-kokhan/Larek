using System;
using Cursors;
using Kitchen.Products;
using Kitchen.Refrigerator.Products;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Kitchen.Refrigerator.Components
{
    public class RefrigeratorProductComponent : MonoBehaviour, IProductComponent, IPointerClickHandler
    {
        public event Action<ProductState> ProductTaken;
        public event Action<ProductState> ProductReturned;

        [Inject] private ProductHolder _productHolder;
        [Inject] private CursorView _cursorView;
        
        private ProductState _productState;
        private bool _isInitialized;

        private int CurrentQuantity
        {
            get => _productState.Quantity;
            set => _productState.Quantity = value;
        }

        public void Initialize(ProductState productState)
        {
            if (_isInitialized) return;
            
            _productState = productState;
            
            _isInitialized = true;
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                OnLeftButtonClicked();
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                OnRightButtonClicked();
            }
        }

        private void OnLeftButtonClicked()
        {
            var product = _productState.Product;
            
            if (CurrentQuantity < 1 || _productHolder.TryTakeNewProduct(product) == false)
            {
                if (product != _productHolder.Product) return;
                
                _productHolder.DeleteHoldingProduct();
                CurrentQuantity += 1;
                
                _cursorView.ClearCursorProductIcon();      
                ProductReturned?.Invoke(_productState);
                
                return;
            }

            CurrentQuantity -= 1;
            
            _cursorView.SetCursorProductIcon(product.PickupCursorIcon);
            ProductTaken?.Invoke(_productState);
        }

        private void OnRightButtonClicked()
        {
            // 
        }
    }
}