using System;
using Kitchen.Products;
using Kitchen.Products.Enums;

namespace Cursors
{
    public class ProductHolder
    {
        public event Action<Product> ProductTaken;
        public event Action ProductReturned;
        
        private Product _product;
        
        public bool IsEmpty => _product == null;

        public bool TryTakeNewProduct(Product product)
        {
            if (IsEmpty == false) return false;
            
            _product = product;
            ProductTaken?.Invoke(_product);
            
            return true;
        }

        public bool TryReturnProduct(Func<ProductType, bool> canAcceptProduct, out Product product)
        {
            product = null;

            if (IsEmpty) return false;
            
            if (canAcceptProduct(_product.Type) == false) return false;
            
            product = _product;
            _product = null;
            
            ProductReturned?.Invoke();
            
            return true;
        }
    }
}