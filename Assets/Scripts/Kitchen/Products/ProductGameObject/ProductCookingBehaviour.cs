using System;
using UnityEngine;

namespace Kitchen.Products.ProductGameObject
{
    [RequireComponent(typeof(ProductObject))]
    public class ProductCookingBehaviour : MonoBehaviour
    {
        public event Action<ProductId> ProductUpdateRequested;
        
        private ProductObject _productObject;
        
        public Product Product => _productObject.Product;

        private CookingType? _currentCookingType;
        private float _cookingTimer;

        private void Awake()
        {
            _productObject = GetComponent<ProductObject>();
        }

        private void Update()
        {
            if (_currentCookingType is null) return;
            
            _cookingTimer += Time.deltaTime;
            
            var requiredTime = GetCookingTime(_currentCookingType.Value);
            
            if (_cookingTimer >= requiredTime)
            {
                UpdateCookingStage();
                _cookingTimer = 0f;
            }
        }

        public void StartCookingProcess(CookingType cookingType)
        {
            _currentCookingType = cookingType;
        }

        public void PauseCookingProcess()
        {
            _currentCookingType = null;
        }

        private float GetCookingTime(CookingType cookingType) => cookingType switch
        {
            CookingType.Frying => Product.FryingTime,
            CookingType.Baking => Product.BakingTime,
            _ => float.MaxValue
        };
        
        private void UpdateCookingStage()
        {
            var product = _productObject.Product;
            
            var newId = new ProductId(product.Type, 
                product.NextCookingStage, 
                product.ChoppingStage);
            
            ProductUpdateRequested?.Invoke(newId);
        }
    }
}