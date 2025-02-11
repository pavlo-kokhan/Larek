using Characters;
using Core;
using Core.Panels;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class FactoriesInstaller : MonoInstaller
    {
        [Header("Interactive Panels")]
        [SerializeField] private Transform _interactivePanelsContainer;
        
        [Header("Characters")]
        [SerializeField] private Transform _charactersContainer;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _interactionPoint;
        [SerializeField] private Transform _leavePoint;
        
        public override void InstallBindings()
        {
            Container.Bind<GameobjectFactory>()
                .AsSingle()
                .WithArguments(Container);
            
            Container.Bind<InteractivePanelsFactory>()
                .AsSingle()
                .WithArguments(Container, _interactivePanelsContainer);
            
            Container.Bind<CharactersFactory>()
                .AsSingle()
                .WithArguments(Container, _charactersContainer, _spawnPoint, _interactionPoint, _leavePoint);
        }
    }
}