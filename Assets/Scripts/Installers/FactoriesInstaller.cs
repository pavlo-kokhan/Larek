using Characters;
using Core;
using Core.Panels;
using Kitchen.Products;
using Kitchen.Products.ProductGameObject;
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

        [Header("Products")] 
        [SerializeField] private Transform _onTableProductsContainer;
        
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
                .WithArguments(Container, _charactersContainer, _spawnPoint);
            
            Container.Bind<ProductObjectsFactory>()
                .AsSingle()
                .WithArguments(Container, _onTableProductsContainer);
        }
    }
}