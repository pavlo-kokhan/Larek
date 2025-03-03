using UnityEngine;
using Zenject;

namespace Kitchen.Products.ProductGameObject
{
    public class ProductObjectsFactory : IFactory<Product, Vector3, GameObject>
    {
        private readonly DiContainer _container;
        private readonly Transform _parent;

        public ProductObjectsFactory(DiContainer container, Transform parent)
        {
            _container = container;
            _parent = parent;
        }

        public GameObject Create(Product product, Vector3 position)
        {
            if (product is null)
            {
                Debug.LogWarning($"{nameof(product)} is null.");
                return null;
            }
            
            var prefab = product.Prefab;
            var instance = _container.InstantiatePrefab(prefab, _parent);
            
            instance.transform.position = position;
            instance.transform.rotation = Quaternion.identity;

            if (!instance.TryGetComponent<ProductObject>(out var productObject))
            {
                Debug.LogWarning($"Missing {nameof(ProductObject)} for {nameof(prefab)}.");
                return null;
            }

            productObject.Initialize(product);

            return instance;
        }
    }
}