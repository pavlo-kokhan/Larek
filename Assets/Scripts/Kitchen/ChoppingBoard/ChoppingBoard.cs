using System.Collections.Generic;
using System.Linq;
using Core;
using Kitchen.Products;
using Kitchen.Products.ProductGameObject;
using Kitchen.Table;
using UnityEngine;
using Zenject;

namespace Kitchen.ChoppingBoard
{
    [RequireComponent(typeof(Collider2D))]
    public class ChoppingBoard : ClickableObjectWithUI
    {
        [SerializeField] private ProductSpawnArea _productSpawnArea;
        
        private readonly List<ProductObject> _registeredProducts = new();
        public ProductObject FirstProductObject => _registeredProducts.FirstOrDefault();

        private ProductHolder _productHolder;

        [Inject]
        private void Construct(ProductHolder productHolder)
        {
            _productHolder = productHolder;
        }
        
        protected override void OnPanelLoaded()
        {
            if (!_interactivePanelInstance.TryGetComponent<ChoppingBoardPanel>(out var panel))
            {
                Debug.LogWarning($"{nameof(_interactivePanelInstance)} requires {nameof(ChoppingBoardPanel)} component.");
                return;
            }
            
            panel.Initialize(this);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<ProductObject>(out var productObject))
            {
                if (_registeredProducts.Contains(productObject)) return;

                if (productObject.Product.OnChoppingBoardPanelSprite is not null)
                {
                    _registeredProducts.Add(productObject);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent<ProductObject>(out var productObject))
            {
                _registeredProducts.Remove(productObject);
            }
        }

        protected override void OnLeftButtonClicked()
        {
            if (!_productHolder.IsEmpty)
            {
                _productSpawnArea.SpawnProduct();
                return;
            }
            
            base.OnLeftButtonClicked();
        }
    }
}