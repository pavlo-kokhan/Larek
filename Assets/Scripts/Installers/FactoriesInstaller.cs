using Characters;
using Core;
using Core.Panels;
using Kitchen.Products.ProductGameObject;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class FactoriesInstaller : MonoInstaller
    {
        [Header("Panels Containers")]
        [SerializeField] private Transform _middleCenterPanelsContainer;
        [SerializeField] private Transform _stretchPanelsContainer;
        
        [Header("Characters")]
        [SerializeField] private Transform _charactersContainer;
        [SerializeField] private Transform _spawnPoint;

        [Header("Products")] 
        [SerializeField] private Transform _onTableProductsContainer;
        
        public override void InstallBindings()
        {
            Container.Bind<GameobjectFactory>()
                .AsSingle()
                .WithArguments(Container);
            
            var interactivePanelsFactory = new InteractivePanelsFactory(Container, 
                _middleCenterPanelsContainer, _stretchPanelsContainer);
            
            Container.Bind<InteractivePanelsFactory>()
                .FromInstance(interactivePanelsFactory)
                .AsSingle();
            
            Container.Bind<CharactersFactory>()
                .AsSingle()
                .WithArguments(Container, _charactersContainer, _spawnPoint);
            
            Container.Bind<ProductObjectsFactory>()
                .AsSingle()
                .WithArguments(Container, _onTableProductsContainer);
        }
    }
}