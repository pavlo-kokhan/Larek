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
            Container.Bind<CursorView>()
                .ToSelf()
                .AsSingle()
                .WithArguments(_availableCursorTextures, _defaultCursorTexture, _productIcon);
        }
    }
}