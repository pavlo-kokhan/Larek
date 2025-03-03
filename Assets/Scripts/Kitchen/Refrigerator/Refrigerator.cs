using Core;
using Kitchen.Products;
using UnityEngine;
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

            if (panel is null)
            {
                Debug.LogWarning($"{nameof(_interactivePanelInstance)} requires component {nameof(RefrigeratorPanel)}");
                return;
            }

            panel.Initialize(_productsStorage.Products);
        }
    }
}