using Core;
using Cursors;
using Data;
using Dialogs;
using GlobalAudio;
using Panels;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField] private Canvas _uiCanvas;
        [SerializeField] private AudioSource _globalAudioSource;
        [SerializeField] private GameObject _dialoguePanel;
        
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

            Container.Bind<ProductHolder>()
                .AsSingle();
            
            Container.Bind<DialogueService>()
                .AsSingle()
                .WithArguments(_dialoguePanel);
        }
    }
}