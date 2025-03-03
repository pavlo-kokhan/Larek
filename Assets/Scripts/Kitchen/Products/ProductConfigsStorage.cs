using System.Collections.Generic;
using Kitchen.Products.Enums;
using UnityEngine;

namespace Kitchen.Products
{
    [CreateAssetMenu(fileName = "Products Storage", menuName = "Scriptable Objects/Configs/Products Configs Storage")]
    public class ProductConfigsStorage : ScriptableObject
    {
        [SerializeField] private List<ProductConfig> _allConfigs = new();

        private readonly Dictionary<ProductId, ProductConfig> _configLookup = new();

        private void OnEnable()
        {
            _configLookup.Clear();
    
            foreach (var config in _allConfigs)
            {
                _configLookup[config.Id] = config;
            }
        }

        public ProductConfig GetConfig(ProductType type, ProductCookingStage cookingStage, ProductChoppingStage choppingStage)
        {
            var productId = new ProductId(type, cookingStage, choppingStage);
            
            return GetConfig(productId);
        }
        
        public ProductConfig GetConfig(ProductId productId)
        {
            return _configLookup[productId];
        }
    }
}