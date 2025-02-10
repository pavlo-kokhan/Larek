using Kitchen.Products;
using Kitchen.Products.Enums;
using Newtonsoft.Json;

namespace Data.PersistantData
{
    [System.Serializable]
    public class ProductSaveData
    {
        public ProductType Type { get; private set; }
        public ProductFryingStage FryingStage { get; private set; }
        public ProductLocation Location { get; private set; }

        public ProductSaveData(Product product)
        {
            Type = product.Type;
            FryingStage = product.FryingStage;
            Location = product.Location;
        }
        
        [JsonConstructor]
        public ProductSaveData(ProductType type, ProductFryingStage fryingStage, ProductLocation location)
        {
            Type = type;
            FryingStage = fryingStage;
            Location = location;
        }
    }
}