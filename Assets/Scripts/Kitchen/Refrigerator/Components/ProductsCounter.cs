using TMPro;
using UnityEngine;

namespace Kitchen.Refrigerator.Components
{
    public class ProductsCounter : MonoBehaviour
    {
        [SerializeField] private RefrigeratorProductComponent _productComponent;
        [SerializeField] private TextMeshProUGUI _textMeshPro;
        
        // private bool _isInitialized;
        //
        // private void OnEnable()
        // {
        //     _productComponent.ProductTaken += UpdateCounter;
        //     _productComponent.ProductReturned += UpdateCounter;
        // }
        //
        // private void OnDisable()
        // {
        //     _productComponent.ProductTaken += UpdateCounter;
        //     _productComponent.ProductReturned += UpdateCounter;
        // }
        //
        // public void Initialize(ProductState productState)
        // {
        //     if (_isInitialized) return;
        //     
        //     UpdateCounter(productState);
        //     
        //     _isInitialized = true;
        // }
        //
        // private void UpdateCounter(ProductState productState)
        // {
        //     _textMeshPro.SetText(productState.Quantity.ToString());
        // }
    }
}