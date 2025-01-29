using System.Collections.Generic;
using Core;
using Kitchen.Products;
using Kitchen.Refrigerator.Components;
using Kitchen.Refrigerator.Products;
using UnityEngine;
using Zenject;

namespace Kitchen.Refrigerator
{
    public class RefrigeratorPanel : MonoBehaviour
    {
        [SerializeField] private List<Transform> _productContainers;
        [Inject] private GameobjectFactory _gameobjectFactory;

        public void InstantiateProductPrefabs(List<ProductState> productStates)
        {
            if (productStates.Count > _productContainers.Count)
            {
                Debug.LogWarning($"{nameof(productStates)} count is greater than {_productContainers} count. " +
                                 "Not all products will be instantiated.", this);
            }

            for (int i = 0; i < productStates.Count && i < _productContainers.Count; i++)
            {
                var productState = productStates[i];
                var prefab = productState.Product.PrefabForRefrigerator;
                
                var instance = _gameobjectFactory.Create(prefab, _productContainers[i]);
                
                var productComponent = instance.GetComponent<RefrigeratorProductComponent>();
                var productCounter = instance.GetComponent<ProductsCounter>();

                productComponent.Initialize(productState);
                productCounter.Initialize(productState);
            }
        }

        // private GameObject GetProductPrefab(ProductState productState)
        // {
        //     var product = productState.Product;
        //     
        //     switch (product.Type)
        //     {
        //         case ProductType.Tomato:
        //             return _productPrefabs.TomatoPrefab;
        //         case ProductType.Salad:
        //             return _productPrefabs.SaladPrefab;
        //         case ProductType.Beef:
        //             return _productPrefabs.BeefPrefab;
        //         case ProductType.Dough:
        //             return _productPrefabs.DoughPrefab;
        //         default:
        //             throw new InvalidOperationException($"Product type {product.Type} has not its prefab.");
        //     }
        // }
    }
}