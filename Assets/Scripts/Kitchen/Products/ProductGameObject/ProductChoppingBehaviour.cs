using System;
using Kitchen.Products.Enums;
using UnityEngine;
using Zenject;

namespace Kitchen.Products.ProductGameObject
{
    [RequireComponent(typeof(ProductObject))]
    public class ProductChoppingBehaviour : MonoBehaviour
    {
        private ProductObject _productObject;
        private ProductObjectsFactory _productObjectsFactory;
        
        private Product Product => _productObject.Product;

        [Inject]
        private void Construct(ProductObjectsFactory productObjectsFactory)
        {
            _productObjectsFactory = productObjectsFactory;
        }
        
        private void Awake() 
        {
            _productObject = GetComponent<ProductObject>();
        }

        public void UpdateChoppingStage()
        {
            var newConfigs = Product.ChoppingParticles;

            if (newConfigs.Count == 0) return;

            var position = transform.position;
            
            foreach (var config in newConfigs)
            {
                var product = new Product(config);
                _productObjectsFactory.Create(product, position);
            }
            
            Destroy(gameObject);
        }
    }
}