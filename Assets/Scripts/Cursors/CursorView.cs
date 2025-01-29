using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Cursors
{
    public class CursorView
    {
        private readonly List<Texture2D> _availableCursorTextures;
        private readonly Texture2D _defaultCursorTexture;
        private readonly Image _productIcon;

        public CursorView(List<Texture2D> availableCursorTextures, Texture2D defaultCursorTexture, Image productIcon)
        {
            _availableCursorTextures = availableCursorTextures;
            _defaultCursorTexture = defaultCursorTexture;
            
            _productIcon = productIcon;
            _productIcon.gameObject.SetActive(true);
            
            SetDefaultCursorTexture();
            ClearCursorProductIcon();
        }


        public void SetCursorProductIcon(Sprite sprite)
        {
            if (sprite == null)
            {
                ClearCursorProductIcon();
                return;
            }
            
            _productIcon.sprite = sprite;
            _productIcon.color = new Color(255, 255, 255, 0.85f);
        }

        public void ClearCursorProductIcon()
        {
            _productIcon.sprite = null;
            _productIcon.color = new Color(255, 255, 255, 0);
        }

        public void SetCursorTexture(Texture2D texture) 
        {
            if (_availableCursorTextures.Contains(texture))
            {
                Cursor.SetCursor(texture, Vector2.zero, CursorMode.Auto);
            }
        }

        public void SetDefaultCursorTexture()
        {
            Cursor.SetCursor(_defaultCursorTexture, Vector2.zero, CursorMode.Auto);
        }
    }
}