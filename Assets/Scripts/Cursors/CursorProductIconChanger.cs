using Refrigerator;
using UnityEngine;
using Zenject;

namespace Cursors
{
    public class CursorProductIconChanger : MonoBehaviour
    {
        [SerializeField] private RefrigeratorProductComponent _productComponent;
        [SerializeField] private Sprite _sprite;
        
        [Inject] private CursorView _cursorView;

        private void OnEnable()
        {
            _productComponent.ProductTaken += OnProductTaken;
            _productComponent.ProductReturned += OnProductReturned;
        }

        private void OnDisable()
        {
            _productComponent.ProductTaken -= OnProductTaken;
            _productComponent.ProductReturned -= OnProductReturned;
        }

        private void OnProductTaken(RefrigeratorProduct product) => _cursorView.SetCursorProductIcon(_sprite);
        private void OnProductReturned(RefrigeratorProduct product) =>  _cursorView.ClearCursorProductIcon();
    }
}