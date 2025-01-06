using UnityEngine;
using Zenject;

namespace Refrigerator
{
    public class RefrigeratorProductsFactory : IFactory<GameObject, Transform, GameObject>
    {
        private readonly DiContainer _container;

        public RefrigeratorProductsFactory(DiContainer container)
        {
            _container = container;
        }

        public GameObject Create(GameObject prefab, Transform parent)
        {
            return _container.InstantiatePrefab(prefab, parent);
        }
    }
}