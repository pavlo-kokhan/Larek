using System;
using System.Collections.Generic;
using System.Linq;
using Core.Cursors;
using Kitchen.Products;
using Kitchen.Products.Enums;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Kitchen.Refrigerator
{
    public class RefrigeratorSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public event Action<int> ProductsCountChanged;
        public event Action ClosePanelRequested;
        
        [field: SerializeField] public ProductType Type { get; private set; }
        [SerializeField] private Texture2D _textureTake;

        private readonly List<Product> _currentProducts = new();
        private IAcceptProductCondition _acceptProductCondition;
        
        private ProductsStorage _productsStorage;
        private ProductHolder _productHolder;
        private CursorView _cursorView;
        
        public int CurrentProductsCount => _currentProducts.Count;

        [Inject]
        public void Construct(ProductsStorage productsStorage, ProductHolder productHolder, CursorView cursorView)
        {
            _acceptProductCondition = new RefrigeratorSlotAcceptCondition(Type);
            
            _productsStorage = productsStorage;
            _productHolder = productHolder;
            _cursorView = cursorView;

            var products = _productsStorage.Products
                .Where(p => p.Config.Type == Type && p.Location == ProductLocation.Refrigerator);

            foreach (var product in products)
            {
                _currentProducts.Add(product);
            }
        }

        private void Start()
        {
            ProductsCountChanged?.Invoke(_currentProducts.Count);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                OnLeftButtonClicked();
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                ClosePanelRequested?.Invoke();
            }
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (CurrentProductsCount == 0) return;
            
            _cursorView.SetCursorTexture(_textureTake);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _cursorView.SetDefaultCursorTexture();
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

            if (_productHolder.TryReturnProduct(_acceptProductCondition, out var product))
            {
                _currentProducts.Add(product);
                ProductsCountChanged?.Invoke(_currentProducts.Count);
            }
        }
    }
}