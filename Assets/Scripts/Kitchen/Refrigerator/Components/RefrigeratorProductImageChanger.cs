using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kitchen.Refrigerator.Components
{
    [RequireComponent(typeof(Image))]
    public class RefrigeratorProductImageChanger : MonoBehaviour
    {
        [SerializeField] private RefrigeratorProductComponent _productComponent;

        [Header("Sprites")] 
        [SerializeField] private Sprite _spriteZeroQuantity;
        [SerializeField] private Sprite _spriteOneQuantity;
        [SerializeField] private Sprite _spriteTwoQuantity;
        [SerializeField] private Sprite _spriteThreeQuantity;
        [SerializeField] private Sprite _spriteFourQuantity;
        [SerializeField] private Sprite _spriteFiveAndMoreQuantity;

        private readonly Dictionary<int, Sprite> _quantitySprites = new();
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        private void Start()
        {
            _quantitySprites.Add(0, _spriteZeroQuantity);
            _quantitySprites.Add(1, _spriteOneQuantity);
            _quantitySprites.Add(2, _spriteTwoQuantity);
            _quantitySprites.Add(3, _spriteThreeQuantity);
            _quantitySprites.Add(4, _spriteFourQuantity);
            _quantitySprites.Add(5, _spriteFiveAndMoreQuantity);
        }

        // private void OnEnable()
        // {
        //     _productComponent.ProductTaken += ChangeRefrigeratorProductImage;
        //     _productComponent.ProductReturned += ChangeRefrigeratorProductImage;
        // }
        //
        // private void OnDisable()
        // {
        //     _productComponent.ProductTaken -= ChangeRefrigeratorProductImage;
        //     _productComponent.ProductReturned -= ChangeRefrigeratorProductImage;
        // }

        // private void ChangeRefrigeratorProductImage(ProductState productState)
        // {
        //     _image.sprite = GetQuantitySprite(productState);
        // }
        //
        // private Sprite GetQuantitySprite(ProductState productState)
        // {
        //     var quantity = productState.Quantity;
        //
        //     if (quantity < 0) return null;
        //     
        //     var key = quantity > 5 ? 5 : quantity;
        //     
        //     return _quantitySprites[key];
        // }
    }
}