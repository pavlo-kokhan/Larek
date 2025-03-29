using System;
using UnityEngine;

namespace Kitchen.Products.ProductGameObject
{
    [RequireComponent(typeof(ProductObject))]
    public class ProductCookingBehaviour : MonoBehaviour
    {
        public event Action<ProductConfig> ProductUpdateRequested;
        
        private ProductObject _productObject;
        private ProductMovement _productMovement;
        
        public Product Product => _productObject.Product;

        private CookingType? _currentCookingType;
        private float _cookingTimer;

        private void Awake()
        {
            _productObject = GetComponent<ProductObject>();
            _productMovement = GetComponent<ProductMovement>();
        }

        private void Update()
        {
            if (_currentCookingType is null || _productMovement.IsMovable) return;
            
            _cookingTimer += Time.deltaTime;
            
            var requiredTime = GetCookingTime();
            
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
        
        private void UpdateCookingStage()
        {
            var newConfig = GetNextConfig();

            if (newConfig is not null)
            {
                ProductUpdateRequested?.Invoke(newConfig);
            }
        }

        private float GetCookingTime()
        {
            if (!_currentCookingType.HasValue) return float.MaxValue;
            
            return _currentCookingType.Value switch
            {
                CookingType.Frying => Product.FryingTime,
                CookingType.Baking => Product.BakingTime,
                _ => float.MaxValue
            };
        }

        private ProductConfig GetNextConfig()
        {
            if (!_currentCookingType.HasValue) return null;
            
            return _currentCookingType.Value switch
            {
                CookingType.Frying => Product.NextFryingConfig,
                CookingType.Baking => Product.NextBakingConfig,
                _ => null
            };
        }
    }
}