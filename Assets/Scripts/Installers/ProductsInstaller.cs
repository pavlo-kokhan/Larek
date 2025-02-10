using Data.SaveLoad;
using Kitchen.Products;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ProductsInstaller : MonoInstaller
    {
        [SerializeField] private ProductConfigsStorage _productConfigsStorage;
        
        public override void InstallBindings()
        {
            IDataService dataService = new JsonDataService();
            var productsStorage = new ProductsStorage(dataService, _productConfigsStorage);
            
            productsStorage.LoadDefaultProducts();

            Container.Bind<ProductConfigsStorage>()
                .FromInstance(_productConfigsStorage)
                .AsSingle();
            
            Container.Bind<ProductsStorage>()
                .FromInstance(productsStorage)
                .AsSingle();
        }
    }
}