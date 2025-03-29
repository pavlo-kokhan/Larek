using UnityEngine;
using Zenject;

namespace Kitchen.Products.ProductGameObject
{
    [RequireComponent(typeof(ProductCookingBehaviour), typeof(ProductChoppingBehaviour))]
    public class ProductObject : MonoBehaviour
    {
        private ProductCookingBehaviour _cookingBehaviour;
        private ProductChoppingBehaviour _choppingBehaviour;

        private bool _isInitialized;
        
        public Product Product { get; private set; }

        private void Awake()
        {
            _cookingBehaviour = GetComponent<ProductCookingBehaviour>();
            _choppingBehaviour = GetComponent<ProductChoppingBehaviour>();
        }
        
        private void OnEnable()
        {
            _cookingBehaviour.ProductUpdateRequested += UpdateProduct;
        }

        private void OnDisable()
        {
            _cookingBehaviour.ProductUpdateRequested -= UpdateProduct;
        }

        public void Initialize(Product product)
        {
            if (_isInitialized) return;

            Product = product;
            
            _isInitialized = true;
        }

        private void UpdateProduct(ProductConfig newConfig)
        {
            if (newConfig is null)
            {
                Debug.LogWarning($"{nameof(newConfig)} can not be null");
                return;
            }
            
            Product = new Product(newConfig);
        }
    }
}