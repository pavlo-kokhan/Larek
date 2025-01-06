using System;
using UnityEngine;
using UnityEngine.UI;

namespace Refrigerator
{
    [RequireComponent(typeof(Image))]
    public class RefrigeratorProductImageChanger : MonoBehaviour
    {
        [SerializeField] private RefrigeratorProductComponent _productComponent;
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        private void OnEnable()
        {
            _productComponent.ProductTaken += ChangeProductImage;
            _productComponent.ProductReturned += ChangeProductImage;
        }

        private void OnDisable()
        {
            _productComponent.ProductTaken -= ChangeProductImage;
            _productComponent.ProductReturned -= ChangeProductImage;
        }

        private void ChangeProductImage(RefrigeratorProduct product)
        {
            _image.sprite = GetQuantitySprite(product);
        }
        
        private Sprite GetQuantitySprite(RefrigeratorProduct product)
        {
            var quantity = product.Quantity;
            
            if (quantity == 0) return product.SpriteZeroQuantity;
            if (quantity == 1) return product.SpriteOneQuantity;
            if (quantity == 2) return product.SpriteTwoQuantity;
            if (quantity == 3) return product.SpriteThreeQuantity;
            if (quantity == 4) return product.SpriteFourQuantity;
            if (quantity >= 5) return product.SpriteFiveAndMoreQuantity;
            
            return null;
        }
    }
}