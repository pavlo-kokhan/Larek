using Kitchen.Products;
using Kitchen.Products.Enums;

namespace Kitchen.Refrigerator
{
    public class RefrigeratorSlotAcceptCondition : IAcceptProductCondition
    {
        private readonly ProductType _productType;

        public RefrigeratorSlotAcceptCondition(ProductType productType)
        {
            _productType = productType;
        }

        public bool CanAcceptProduct(Product product)
        {
            return product.CanBeInFridge && _productType == product.Type;
        }
    }
}