using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using Zenject;

namespace Kitchen.Products
{
    public class ProductHolder
    {
        public event Action<IReadOnlyList<Product>> ProductsUpdated;
        
        private readonly List<Product> _products = new();

        public bool IsEmpty => _products.IsEmpty();

        public bool TryTakeNewProduct(Product product)
        {
            if (product is null) return false;

            if (_products.Count == 0)
            {
                AddProduct(product);
                return true;
            }
            
            if (product.Config.Equals(_products.Last().Config) 
                || _products.Count >= product.SlicesCount) return false;
            
            AddProduct(product);
            return true;
        }

        private void AddProduct(Product product)
        {
            _products.Add(product);
            ProductsUpdated?.Invoke(_products.AsReadOnly());
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
            ProductsUpdated?.Invoke(_products.AsReadOnly());
        }
    }
}