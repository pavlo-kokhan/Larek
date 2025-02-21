namespace Kitchen.Products.OnTable
{
    public class ProductSpawnAreaAcceptCondition : IAcceptProductCondition
    {
        public bool CanAcceptProduct(Product product)
        {
            return product.CanBeSpawned && product.Prefab is not null;
        }
    }
}