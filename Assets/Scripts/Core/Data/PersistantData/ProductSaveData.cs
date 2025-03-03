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
        public ProductChoppingStage ChoppingStage { get; private set; }

        public ProductSaveData(Product product)
        {
            Type = product.Type;
            CookingStage = product.CookingStage;
        }
        
        [JsonConstructor]
        public ProductSaveData(ProductType type, ProductCookingStage cookingStage, ProductChoppingStage choppingStage)
        {
            Type = type;
            CookingStage = cookingStage;
            ChoppingStage = choppingStage;
        }
    }
}