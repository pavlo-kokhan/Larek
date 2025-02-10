using System;
using Core;
using Kitchen.Products;
using Zenject;

namespace Kitchen.Refrigerator
{
    public class Refrigerator : ClickableObjectWithUI
    {
        private ProductsStorage _productsStorage;
        
        [Inject]
        public void Construct(ProductsStorage productsStorage)
        {
            _productsStorage = productsStorage;
        }
        
        protected override void OnPanelLoaded()
        {
            var panel = _interactivePanelInstance.GetComponent<RefrigeratorPanel>();

            if (panel == null)
            {
                throw new InvalidOperationException(
                    $"Component {typeof(RefrigeratorPanel)} does not exist in {nameof(_interactivePanelInstance)}");
            }
        }
    }
}