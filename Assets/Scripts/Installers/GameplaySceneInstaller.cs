using System.Linq;
using Cursors;
using Data;
using GlobalAudio;
using Panels;
using Refrigerator;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField] private Canvas _uiCanvas;
        [SerializeField] private AudioSource _globalAudioSource;
        [SerializeField] private Transform _interactivePanelsContainer;
        
        public override void InstallBindings()
        {
            Container.Bind<Canvas>()
                .FromInstance(_uiCanvas)
                .AsSingle();
            
            Container.Bind<GlobalAudioPlayer>()
                .ToSelf()
                .AsSingle()
                .WithArguments(_globalAudioSource);
            
            Container.Bind<InteractivePanelsRegistrator>()
                .ToSelf()
                .AsSingle();

            Container.Bind<InteractivePanelsFactory>()
                .AsSingle()
                .WithArguments(Container, _interactivePanelsContainer);
            
            IPersistentData persistentData = new PersistentData();
            
            Container.Bind<IPersistentData>()
                .FromInstance(persistentData)
                .AsSingle();
            
            Container.Bind<PlayerDataProvider>()
                .AsSingle()
                .WithArguments(persistentData);
            
            Container.Bind<RefrigeratorDataProvider>()
                .AsSingle()
                .WithArguments(persistentData);

            Container.Bind<RefrigeratorProductsFactory>()
                .AsSingle()
                .WithArguments(Container);

            Container.Bind<ProductHolder>()
                .AsSingle();
        }
    }
}