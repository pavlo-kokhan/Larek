using UnityEngine;
using Zenject;

namespace Core.Panels
{
    public class InteractivePanelsFactory : IFactory<GameObject, GameObject>
    {
        private readonly DiContainer _container;
        private readonly Transform _panelsContainer;

        public InteractivePanelsFactory(DiContainer container, Transform panelsContainer)
        {
            _container = container;
            _panelsContainer = panelsContainer;
        }

        public GameObject Create(GameObject prefab)
        {
            return _container.InstantiatePrefab(prefab, _panelsContainer);
        }
    }
}