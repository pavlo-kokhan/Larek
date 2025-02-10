using System.Collections.Generic;
using Kitchen.Products;
using Newtonsoft.Json;

namespace Data.PersistantData
{
    [System.Serializable]
    public class ProductsSaveData
    {
        private List<ProductSaveData> _products = new();
        
        public IEnumerable<ProductSaveData> Products => _products;

        public ProductsSaveData(List<Product> products)
        {
            foreach (var product in products)
            {
                _products.Add(new ProductSaveData(product));
            }
        }

        [JsonConstructor]
        public ProductsSaveData(List<ProductSaveData> products)
        {
            _products = products;
        }
    }
}