using System;
using System.Collections.Generic;
using System.Linq;
using Core.Cursors;
using Kitchen.Products;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Kitchen.ProductsContainer
{
    [RequireComponent(typeof(Collider2D))]
    public class ProductsContainerSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public event Action<IReadOnlyList<Product>> ProductsCountChanged;
        
        [SerializeField] private Texture2D _textureTake;

        private readonly List<Product> _currentProducts = new();
        private IAcceptProductCondition _acceptProductCondition;
        
        private ProductHolder _productHolder;
        private CursorView _cursorView;
        
        [Inject]
        private void Construct(ProductHolder productHolder, CursorView cursorView)
        {
            _acceptProductCondition =
                new ProductsContainerSlotAcceptCondition(_currentProducts.AsReadOnly());
            
            _productHolder = productHolder;
            _cursorView = cursorView;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                OnLeftButtonClicked();
            }
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_currentProducts.Count == 0) return;
            
            _cursorView.SetCursorTexture(_textureTake);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _cursorView.SetDefaultCursorTexture();
        }
        
        private void OnLeftButtonClicked()
        {
            if (_productHolder.TryReturnProduct(_acceptProductCondition, out var product))
            {
                _currentProducts.Add(product);
                ProductsCountChanged?.Invoke(_currentProducts.AsReadOnly());
                return;
            }
            
            if (_currentProducts.Count > 0)
            {
                var lastProduct = _currentProducts.Last();

                if (_productHolder.TryTakeNewProduct(lastProduct))
                {
                    _currentProducts.Remove(lastProduct);
                    ProductsCountChanged?.Invoke(_currentProducts.AsReadOnly());
                }
            }
        }
    }
}