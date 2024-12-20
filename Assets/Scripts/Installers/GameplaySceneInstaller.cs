using GlobalAudio;
using Panels;
using Radio;
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
        }
    }
}