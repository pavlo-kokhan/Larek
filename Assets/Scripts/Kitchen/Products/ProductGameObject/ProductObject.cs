using UnityEngine;
using Zenject;

namespace Kitchen.Products.ProductGameObject
{
    [RequireComponent(typeof(ProductCookingBehaviour), typeof(ProductChoppingBehaviour))]
    public class ProductObject : MonoBehaviour
    {
        private ProductCookingBehaviour _cookingBehaviour;
        private ProductChoppingBehaviour _choppingBehaviour;

        private ProductConfigsStorage _productConfigsStorage;
        
        private bool _isInitialized;
        
        public Product Product { get; private set; }

        [Inject]
        public void Construct(ProductConfigsStorage productConfigsStorage)
        {
            _productConfigsStorage = productConfigsStorage;
        }

        private void Awake()
        {
            _cookingBehaviour = GetComponent<ProductCookingBehaviour>();
            _choppingBehaviour = GetComponent<ProductChoppingBehaviour>();
        }

        public void Initialize(Product product)
        {
            if (_isInitialized) return;

            Product = product;
            
            _isInitialized = true;
        }

        private void OnEnable()
        {
            _cookingBehaviour.ProductUpdateRequested += UpdateProduct;
            _choppingBehaviour.ProductUpdateRequested += UpdateProduct;
        }

        private void OnDisable()
        {
            _cookingBehaviour.ProductUpdateRequested -= UpdateProduct;
            _choppingBehaviour.ProductUpdateRequested -= UpdateProduct;
        }

        private void UpdateProduct(ProductId newId)
        {
            var newConfig = _productConfigsStorage.GetConfig(newId);
            
            if (newConfig is null)
            {
                Debug.LogWarning($"No config for type {newId}");
                return;
            }
            
            Product = new Product(newConfig);
        }
    }
}