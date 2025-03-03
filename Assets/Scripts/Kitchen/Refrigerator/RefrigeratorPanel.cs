using System.Collections.Generic;
using Core.Panels;
using Kitchen.Products;
using UnityEngine;

namespace Kitchen.Refrigerator
{
    [RequireComponent(typeof(ClosablePanel))]
    public class RefrigeratorPanel : MonoBehaviour
    {
        [SerializeField] private List<RefrigeratorSlot> _productSlots;

        private ClosablePanel _closablePanelComponent;
        private bool _isInitialized;

        private void OnEnable()
        {
            foreach (var productSlot in _productSlots)
            {
                productSlot.ClosePanelRequested += ClosePanel;
            }
        }

        private void OnDisable()
        {
            foreach (var productSlot in _productSlots)
            {
                productSlot.ClosePanelRequested -= ClosePanel;
            }
        }

        private void Awake()
        {
            _closablePanelComponent = GetComponent<ClosablePanel>();
        }
        
        public void Initialize(IEnumerable<Product> products)
        {
            if (_isInitialized) return;

            foreach (var slot in _productSlots)
            {
                slot.Initialize(products);
            }
            
            _isInitialized = true;
        }

        private void ClosePanel()
        {
            _closablePanelComponent.ClosePanel();
        }
    }
}