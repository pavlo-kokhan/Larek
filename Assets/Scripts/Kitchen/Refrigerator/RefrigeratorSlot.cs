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
        public event Action<IReadOnlyCollection<Product>> ProductsChanged;
        public event Action ClosePanelRequested;
        
        [field: SerializeField] public ProductId ProductId { get; private set; }
        [SerializeField] private Texture2D _textureTake;

        private readonly List<Product> _currentProducts = new();
        public IEnumerable<Product> CurrentProducts => _currentProducts;
        
        private IAcceptProductCondition _acceptCondition;
        
        private ProductsStorage _productsStorage;
        private ProductHolder _productHolder;
        private CursorView _cursorView;

        private bool _isInitialized;
        
        private int CurrentProductsCount => _currentProducts.Count;

        [Inject]
        private void Construct(ProductsStorage productsStorage, ProductHolder productHolder, CursorView cursorView)
        {
            _acceptCondition = new RefrigeratorSlotAcceptCondition(ProductId);
            
            _productsStorage = productsStorage;
            _productHolder = productHolder;
            _cursorView = cursorView;
        }

        private void Start()
        {
            ProductsChanged?.Invoke(_currentProducts.AsReadOnly());
        }

        public void Initialize(IEnumerable<Product> products)
        {
            if (_isInitialized) return;

            foreach (var product in products)
            {
                if (_acceptCondition.CanAcceptProduct(product)) _currentProducts.Add(product);
            }
            
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
                    ProductsChanged?.Invoke(_currentProducts.AsReadOnly());
                    return;
                }
            }

            if (_productHolder.TryReturnProduct(_acceptCondition, out var product))
            {
                _currentProducts.Add(product);
                ProductsChanged?.Invoke(_currentProducts.AsReadOnly());
            }
        }
    }
}