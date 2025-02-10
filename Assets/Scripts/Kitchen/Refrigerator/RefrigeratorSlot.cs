using System;
using System.Collections.Generic;
using System.Linq;
using Cursors;
using Kitchen.Products;
using Kitchen.Products.Enums;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Kitchen.Refrigerator
{
    public class RefrigeratorSlot : MonoBehaviour, IPointerClickHandler
    {
        public event Action<int> ProductsCountChanged;
        public event Action RightButtonClicked;
        
        [field: SerializeField] public ProductType Type { get; private set; }

        private readonly List<Product> _currentProducts = new();
        private ProductsStorage _productsStorage;
        private ProductHolder _productHolder;
        
        public int CurrentProductsCount => _currentProducts.Count;

        [Inject]
        public void Construct(ProductsStorage productsStorage, ProductHolder productHolder)
        {
            _productsStorage = productsStorage;
            _productHolder = productHolder;

            var products = _productsStorage.Products
                .Where(p => p.Type == Type && p.Location == ProductLocation.Refrigerator);

            foreach (var product in products)
            {
                _currentProducts.Add(product);
            }
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                OnLeftButtonClicked();
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                RightButtonClicked?.Invoke();
            }
        }

        private void OnLeftButtonClicked()
        {
            if (_currentProducts.Count > 0)
            {
                var lastProduct = _currentProducts.Last();

                if (_productHolder.TryTakeNewProduct(lastProduct))
                {
                    _currentProducts.Remove(lastProduct);
                    ProductsCountChanged?.Invoke(_currentProducts.Count);
                    return;
                }
            }

            if (_productHolder.TryReturnProduct(CanAcceptProduct, out var product))
            {
                _currentProducts.Add(product);
                ProductsCountChanged?.Invoke(_currentProducts.Count);
            }
        }

        private bool CanAcceptProduct(ProductType productType) => productType == Type;
    }
}