using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Refrigerator
{
    public class RefrigeratorPanel : MonoBehaviour
    {
        [Header("Product Prefabs")]
        [SerializeField] private GameObject _tomatoPrefab;
        [SerializeField] private GameObject _saladPrefab;
        [SerializeField] private GameObject _beefPrefab;
        [SerializeField] private GameObject _doughPrefab;
        
        [SerializeField] private List<Transform> _productContainers;
        [Inject] private RefrigeratorProductsFactory _productsFactory;

        public void InstantiateProductPrefabs(List<RefrigeratorProduct> products)
        {
            if (products.Count > _productContainers.Count)
            {
                Debug.LogWarning($"{nameof(products)} count is greater than {_productContainers} count. " +
                                 "Not all products will be instantiated.", this);
            }

            for (int i = 0; i < products.Count && i < _productContainers.Count; i++)
            {
                var product = products[i];
                var prefab = GetProductPrefab(product);
                
                var instance = _productsFactory.Create(prefab, _productContainers[i]);
                
                var productComponent = instance.GetComponent<RefrigeratorProductComponent>();
                var productCounter = instance.GetComponent<RefrigeratorProductCounter>();
                
                productComponent.Initialize(product);
                productCounter.Initialize(product);
            }
        }

        private GameObject GetProductPrefab(RefrigeratorProduct product)
        {
            switch (product.Type)
            {
                case RefrigeratorProductType.Tomato:
                    return _tomatoPrefab;
                case RefrigeratorProductType.Salad:
                    return _saladPrefab;
                case RefrigeratorProductType.Beef:
                    return _beefPrefab;
                case RefrigeratorProductType.Dough:
                    return _doughPrefab;
                default:
                    throw new InvalidOperationException($"Product type {product.Type} has not its prefab.");
            }
        }
    }
}