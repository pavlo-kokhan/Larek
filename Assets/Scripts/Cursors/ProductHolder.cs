using Refrigerator;

namespace Cursors
{
    public class ProductHolder
    {
        private RefrigeratorProduct _product;
        public RefrigeratorProduct Product => _product;

        public bool TryTakeNewProduct(RefrigeratorProduct product)
        {
            if (_product != null) return false;
            _product = product;
            return true;
        }
        
        public void ReturnHoldingProduct() => _product = null;
    }
}