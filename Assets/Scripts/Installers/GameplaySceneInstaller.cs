using Core.Dialogs;
using Core.GlobalAudio;
using Core.Panels;
using Core.RoomSidesSwitcherComponents;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField] private Canvas _uiCanvas;
        [SerializeField] private RoomSidesSwitcher _roomSidesSwitcher;
        [SerializeField] private AudioSource _globalAudioSource;
        [SerializeField] private DialogueChoicesPanel _dialogueChoicesPanel;
        
        public override void InstallBindings()
        {
            Container.Bind<Canvas>()
                .FromInstance(_uiCanvas)
                .AsSingle();
            
            Container.Bind<GlobalAudioPlayer>()
                .ToSelf()
                .AsSingle()
                .WithArguments(_globalAudioSource);

            var interactivePanelsRegistrator = new InteractivePanelsRegistrator(_roomSidesSwitcher);
            
            Container.Bind<InteractivePanelsRegistrator>()
                .FromInstance(interactivePanelsRegistrator)
                .AsSingle();
            
            Container.Bind<DialogueService>()
                .AsSingle()
                .WithArguments(_dialogueChoicesPanel);
        }
    }
}