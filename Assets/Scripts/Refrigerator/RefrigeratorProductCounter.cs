using TMPro;
using UnityEngine;

namespace Refrigerator
{
    public class RefrigeratorProductCounter : MonoBehaviour
    {
        [SerializeField] private RefrigeratorProductComponent _productComponent;
        [SerializeField] private TextMeshProUGUI _textMeshPro;
        
        private bool _isInitialized;

        private void OnEnable()
        {
            _productComponent.ProductTaken += UpdateCounter;
            _productComponent.ProductReturned += UpdateCounter;
        }

        private void OnDisable()
        {
            _productComponent.ProductTaken += UpdateCounter;
            _productComponent.ProductReturned += UpdateCounter;
        }

        private void Awake()
        {
            _textMeshPro = GetComponent<TextMeshProUGUI>();
        }

        public void Initialize(RefrigeratorProduct product)
        {
            if (_isInitialized) return;
            
            UpdateCounter(product);
            
            _isInitialized = true;
        }

        private void UpdateCounter(RefrigeratorProduct product) => _textMeshPro.SetText(product.Quantity.ToString());
    }
}