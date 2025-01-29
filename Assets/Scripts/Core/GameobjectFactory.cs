using UnityEngine;
using Zenject;

namespace Core
{
    public class GameobjectFactory : IFactory<GameObject, Transform, GameObject>
    {
        private readonly DiContainer _container;

        public GameobjectFactory(DiContainer container)
        {
            _container = container;
        }

        public GameObject Create(GameObject prefab, Transform parent)
        {
            return _container.InstantiatePrefab(prefab, parent);
        }
    }
}