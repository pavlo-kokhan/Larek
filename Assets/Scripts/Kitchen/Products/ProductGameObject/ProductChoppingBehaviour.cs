using System;
using UnityEngine;

namespace Kitchen.Products.ProductGameObject
{
    [RequireComponent(typeof(ProductObject))]
    public class ProductChoppingBehaviour : MonoBehaviour
    {
        public event Action<ProductId> ProductUpdateRequested;
        
        private ProductObject _productObject;
        
        public Product Product => _productObject.Product;
        
        private void Awake()
        {
            _productObject = GetComponent<ProductObject>();
        }

        public void UpdateChoppingStage()
        {
            var product = _productObject.Product;
            
            var newId = new ProductId(product.Type, 
                product.CookingStage, 
                product.NextChoppingStage);
            
            ProductUpdateRequested?.Invoke(newId);
        }
    }
}