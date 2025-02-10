using System.Collections.Generic;
using Kitchen.Products.Enums;
using UnityEngine;

namespace Kitchen.Products
{
    [CreateAssetMenu(fileName = "Products Storage", menuName = "Scriptable Objects/Configs/Products Configs Storage")]
    public class ProductConfigsStorage : ScriptableObject
    {
        [field: SerializeField] public List<ProductConfig> AllConfigs { get; private set; } = new();

        private readonly Dictionary<(ProductType, ProductFryingStage), ProductConfig> _configLookup = new();

        private void OnEnable()
        {
            _configLookup.Clear();
    
            foreach (var config in AllConfigs)
            {
                _configLookup[(config.Type, config.FryingStage)] = config;
            }
        }

        public ProductConfig GetConfigOfProductType(ProductType type, ProductFryingStage fryingStage)
        {
            return _configLookup.GetValueOrDefault((type, fryingStage));
        }

        public ProductConfig GetConfigOfNextFryingStageType(ProductType type, ProductFryingStage fryingStage)
        {
            return null;
        }
    }
}