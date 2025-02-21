using Kitchen.Products;
using Kitchen.Products.Enums;
using Newtonsoft.Json;

namespace Core.Data.PersistantData
{
    [System.Serializable]
    public class ProductSaveData
    {
        public ProductType Type { get; private set; }
        public ProductCookingStage CookingStage { get; private set; }
        public ProductLocation Location { get; private set; }

        public ProductSaveData(Product product)
        {
            Type = product.Config.Type;
            CookingStage = product.Config.CookingStage;
            Location = product.Location;
        }
        
        [JsonConstructor]
        public ProductSaveData(ProductType type, ProductCookingStage cookingStage, ProductLocation location)
        {
            Type = type;
            CookingStage = cookingStage;
            Location = location;
        }
    }
}