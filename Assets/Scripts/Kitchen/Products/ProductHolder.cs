using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;

namespace Kitchen.Products
{
    public class ProductHolder
    {
        public event Action<Product> ProductTaken;
        public event Action ProductReturned;
        
        private readonly List<Product> _products = new();

        public bool HoldsProduct => !_products.IsEmpty();
        
        public bool TryTakeNewProduct(Product product)
        {
            if (product is null) return false;

            if (_products.Count == 0)
            {
                AddProduct(product);
                return true;
            }
            
            if (product.IsNotTheSameAs(_products.Last()) 
                || _products.Count >= product.SlicesCount) return false;
            
            AddProduct(product);
            return true;
        }

        private void AddProduct(Product product)
        {
            _products.Add(product);
            ProductTaken?.Invoke(product);
        }

        public bool TryReturnProduct(IAcceptProductCondition acceptCondition, out Product product)
        {
            product = null;

            if (_products.IsEmpty()) return false;
            if (acceptCondition.CanAcceptProduct(_products.Last()) == false) return false;
            
            product = _products.Last();
            RemoveProduct(product);
            
            return true;
        }

        private void RemoveProduct(Product product)
        {
            _products.Remove(product);
            ProductReturned?.Invoke();
        }
    }
}