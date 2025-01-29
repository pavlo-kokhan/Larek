using System.Collections.Generic;
using Kitchen.Refrigerator.Products;

namespace Kitchen.Refrigerator.Content
{
    public class RefrigeratorContentState
    {
        private readonly List<ProductState> _productStates = new();
        
        public IEnumerable<ProductState> ProductStates => _productStates;

        public RefrigeratorContentState(RefrigeratorContent refrigeratorContent)
        {
            foreach (var product in refrigeratorContent.Products)
            {
                _productStates.Add(new ProductState(product));
            }
        }
    }
}