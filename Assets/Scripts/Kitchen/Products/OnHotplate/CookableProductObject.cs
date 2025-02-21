using Kitchen.Products.Enums;
using UnityEngine;
using Zenject;

namespace Kitchen.Products.OnHotplate
{
    public class CookableProductObject : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _productSpriteRenderer;
        [SerializeField] private SpriteRenderer _juiceSpriteRenderer;
        
        private Product _product;
        private bool _isInitialized;
        
        private ProductConfigsStorage _productConfigsStorage;

        [Inject]
        public void Construct(ProductConfigsStorage productConfigsStorage)
        {
            _productConfigsStorage = productConfigsStorage;
        }

        public void Initialize(Product product)
        {
            if (_isInitialized) return;
            
            _product = product;
            
            _isInitialized = true;
        }

        public void UpdateCookingStage()
        {
            var newConfig = _productConfigsStorage.GetConfig(_product.Type, _product.GetNextCookingStage(), _product.ChoppingStage);
            var newProduct = new Product(newConfig, ProductLocation.Refrigerator);
            var newProductSprite = newProduct.OnHotplateSprite;
            //
        }
    }
}