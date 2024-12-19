using GlobalAudio;
using Panels;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField] private Canvas _uiCanvas;
        [SerializeField] private Transform _interactivePanelsContainer;
        [SerializeField] private AudioSource _globalAudioSource;
        
        public override void InstallBindings()
        {
            Container.Bind<Canvas>()
                .FromInstance(_uiCanvas)
                .AsSingle();
            
            Container.Bind<InteractivePanelsRegistrator>()
                .ToSelf()
                .AsSingle();

            Container.Bind<InteractivePanelsFactory>()
                .AsSingle()
                .WithArguments(Container, _interactivePanelsContainer);
            
            Container.Bind<GlobalAudioPlayer>()
                .ToSelf()
                .AsSingle()
                .WithArguments(_globalAudioSource);
        }
    }
}