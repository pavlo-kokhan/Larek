using System.Collections.Generic;
using Kitchen.Products.Enums;
using UnityEngine;

namespace Kitchen.Products
{
    [CreateAssetMenu(fileName = "Products Storage", menuName = "Scriptable Objects/Configs/Products Configs Storage")]
    public class ProductConfigsStorage : ScriptableObject
    {
        [field: SerializeField] public List<ProductConfig> AllConfigs { get; private set; } = new();

        private readonly Dictionary<(ProductType, ProductCookingStage, ProductChoppingStage), ProductConfig> _configLookup = new();

        private void OnEnable()
        {
            _configLookup.Clear();
    
            foreach (var config in AllConfigs)
            {
                _configLookup[(config.Type, config.CookingStage, config.ChoppingStage)] = config;
            }
        }

        public ProductConfig GetConfig(ProductType type, ProductCookingStage cookingStage, ProductChoppingStage choppingStage)
        {
            return _configLookup.GetValueOrDefault((type, cookingStage, choppingStage));
        }
    }
}