using System.Collections.Generic;
using Cursors;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Installers
{
    public class CursorInstaller : MonoInstaller
    {
        [SerializeField] private List<Texture2D> _availableCursorTextures;
        [SerializeField] private Texture2D _defaultCursorTexture;
        [SerializeField] private Image _productIcon;
        
        public override void InstallBindings()
        {
            var productHolder = new ProductHolder();
            var cursorView = new CursorView(_availableCursorTextures, _defaultCursorTexture, _productIcon, productHolder);
            
            Container.Bind<ProductHolder>()
                .FromInstance(productHolder)
                .AsSingle();
            
            Container.Bind<CursorView>()
                .FromInstance(cursorView)
                .AsSingle();
        }
    }
}