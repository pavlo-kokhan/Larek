using Kitchen.Products;
using Kitchen.Products.Enums;

namespace Kitchen.Refrigerator
{
    public class RefrigeratorSlotAcceptCondition : IAcceptProductCondition
    {
        private readonly ProductId _productId;

        public RefrigeratorSlotAcceptCondition(ProductId productId)
        {
            _productId = productId;
        }

        public bool CanAcceptProduct(Product product)
        {
            return product.Id.Equals(_productId);
        }
    }
}