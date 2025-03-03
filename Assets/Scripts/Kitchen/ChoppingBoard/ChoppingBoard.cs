using System.Collections.Generic;
using System.Linq;
using Core;
using Kitchen.Products.ProductGameObject;
using UnityEngine;

namespace Kitchen.ChoppingBoard
{
    [RequireComponent(typeof(Collider2D))]
    public class ChoppingBoard : ClickableObjectWithUI
    {
        private readonly List<ProductObject> _registeredProducts = new();
        public ProductObject FirstProductObject => _registeredProducts.FirstOrDefault();
        
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
                
                _registeredProducts.Add(productObject);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent<ProductObject>(out var productObject))
            {
                if (!_registeredProducts.Contains(productObject)) return;
                
                _registeredProducts.Remove(productObject);
            }
        }
    }
}