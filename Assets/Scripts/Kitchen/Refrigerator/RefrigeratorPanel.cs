using System.Collections.Generic;
using Panels;
using UnityEngine;

namespace Kitchen.Refrigerator
{
    [RequireComponent(typeof(ClosablePanel))]
    public class RefrigeratorPanel : MonoBehaviour
    {
        [SerializeField] private List<RefrigeratorSlot> _productSlots;

        private ClosablePanel _closablePanelComponent;

        private void OnEnable()
        {
            foreach (var productSlot in _productSlots)
            {
                productSlot.RightButtonClicked += ClosePanel;
            }
        }

        private void OnDisable()
        {
            foreach (var productSlot in _productSlots)
            {
                productSlot.RightButtonClicked -= ClosePanel;
            }
        }

        private void Awake()
        {
            _closablePanelComponent = GetComponent<ClosablePanel>();
        }

        private void ClosePanel()
        {
            _closablePanelComponent.ClosePanel();
        }
    }
}