using System;
using Cursors;
using UnityEngine;
using Zenject;

namespace Refrigerator
{
    public class RefrigeratorProductComponent : MonoBehaviour
    {
        public event Action<RefrigeratorProduct> ProductTaken;
        public event Action<RefrigeratorProduct> ProductReturned;
        
        [SerializeField] private RefrigeratorProductClickHandler _clickHandler;
        [Inject] private ProductHolder _productHolder;
        
        private RefrigeratorProduct _product;
        private bool _isInitialized;
        
        private int CurrentQuantity => _product.Quantity;

        private void OnEnable()
        {
            _clickHandler.LeftButtonClicked += OnLeftButtonClicked;
            _clickHandler.RightButtonClicked += OnRightButtonClicked;
        }

        private void OnDisable()
        {
            _clickHandler.LeftButtonClicked -= OnLeftButtonClicked;
            _clickHandler.RightButtonClicked -= OnRightButtonClicked;
        }

        public void Initialize(RefrigeratorProduct product)
        {
            if (_isInitialized) return;
            
            _product = product;
            
            _isInitialized = true;
        }

        private void OnLeftButtonClicked()
        {
            if (CurrentQuantity < 1 || _productHolder.TryTakeNewProduct(_product) == false)
            {
                if (_product != _productHolder.Product) return;
                
                _productHolder.ReturnHoldingProduct();
                _product.Quantity += 1;
                
                ProductReturned?.Invoke(_product);
                
                return;
            }

            _product.Quantity -= 1;
            
            ProductTaken?.Invoke(_product);
        }

        private void OnRightButtonClicked()
        {
            // 
        }
    }
}