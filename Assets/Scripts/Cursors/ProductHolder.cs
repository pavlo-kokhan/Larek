using Kitchen.Products;

namespace Cursors
{
    public class ProductHolder
    {
        private Product _product;
        public Product Product => _product;

        public bool TryTakeNewProduct(Product product)
        {
            if (_product != null) return false;
            _product = product;
            return true;
        }
        
        public void DeleteHoldingProduct() => _product = null;
    }
}