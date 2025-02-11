using Kitchen.Refrigerator.Components;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Core.Cursors
{
    public class CursorProductIconChanger : MonoBehaviour
    {
        [FormerlySerializedAs("_productComponent")] [SerializeField] private RefrigeratorProductComponent _refrigeratorProductComponent;
        [SerializeField] private Sprite _sprite;
        
        [Inject] private CursorView _cursorView;

        // private void OnEnable()
        // {
        //     _productComponent.ProductTaken += OnProductTaken;
        //     _productComponent.ProductReturned += OnProductReturned;
        // }
        //
        // private void OnDisable()
        // {
        //     _productComponent.ProductTaken -= OnProductTaken;
        //     _productComponent.ProductReturned -= OnProductReturned;
        // }
        //
        // private void OnProductTaken(RefrigeratorProductState productState) => _cursorView.SetCursorProductIcon(_sprite);
        // private void OnProductReturned(RefrigeratorProductState productState) =>  _cursorView.ClearCursorProductIcon();
    }
}