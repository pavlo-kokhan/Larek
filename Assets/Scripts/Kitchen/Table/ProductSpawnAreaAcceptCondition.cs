using Kitchen.Products;

namespace Kitchen.Table
{
    public class ProductSpawnAreaAcceptCondition : IAcceptProductCondition
    {
        public bool CanAcceptProduct(Product product)
        {
            return product.Prefab is not null;
        }
    }
}