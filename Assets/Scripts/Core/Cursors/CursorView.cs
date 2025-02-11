using System;
using System.Collections.Generic;
using Kitchen.Products;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Cursors
{
    public class CursorView : IDisposable
    {
        private readonly List<Texture2D> _availableCursorTextures;
        private readonly Texture2D _defaultCursorTexture;
        private readonly Image _productIcon;
        private readonly ProductHolder _productHolder;
        
        private Texture2D _currentCursorTexture;

        public CursorView(List<Texture2D> availableCursorTextures, Texture2D defaultCursorTexture,
            Image productIcon, ProductHolder productHolder)
        {
            _availableCursorTextures = availableCursorTextures;
            _defaultCursorTexture = defaultCursorTexture;
            _currentCursorTexture = _defaultCursorTexture;
            
            _productIcon = productIcon;
            _productHolder = productHolder;
            _productIcon.gameObject.SetActive(true);
            
            SetDefaultCursorTexture();
            ClearCursorProductIcon();

            _productHolder.ProductTaken += SetCursorProductIcon;
            _productHolder.ProductReturned += ClearCursorProductIcon;
        }
        
        public void Dispose()
        {
            _productHolder.ProductTaken -= SetCursorProductIcon;
            _productHolder.ProductReturned -= ClearCursorProductIcon;
        }
        
        public void SetCursorTexture(Texture2D texture) 
        {
            if (_availableCursorTextures.Contains(texture) && _currentCursorTexture != texture)
            {
                _currentCursorTexture = texture;
                Cursor.SetCursor(texture, Vector2.zero, CursorMode.Auto);
            }
        }

        public void SetDefaultCursorTexture()
        {
            _currentCursorTexture = _defaultCursorTexture;
            Cursor.SetCursor(_defaultCursorTexture, Vector2.zero, CursorMode.Auto);
        }
        
        private void SetCursorProductIcon(Product product)
        {
            var sprite = product.PickupCursorSprite;
            
            if (sprite == null)
            {
                ClearCursorProductIcon();
                return;
            }
            
            _productIcon.sprite = sprite;
            _productIcon.SetNativeSize();
            _productIcon.rectTransform.localScale = new Vector3(0.1f, 0.1f, 1);
            _productIcon.color = new Color(255, 255, 255, 0.85f);
        }

        private void ClearCursorProductIcon()
        {
            _productIcon.sprite = null;
            _productIcon.color = new Color(255, 255, 255, 0);
            _productIcon.rectTransform.sizeDelta = new Vector2(50f, 50f);
        }
    }
}