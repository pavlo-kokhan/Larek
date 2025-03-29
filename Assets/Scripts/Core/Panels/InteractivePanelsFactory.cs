using UnityEngine;
using Zenject;

namespace Core.Panels
{
    public class InteractivePanelsFactory : IFactory<GameObject, ContainerType, GameObject>
    {
        private readonly DiContainer _container;
        private readonly Transform _middleCenterContainer;
        private readonly Transform _stretchContainer;

        public InteractivePanelsFactory(DiContainer container, Transform middleCenterContainer, Transform stretchContainer)
        {
            _container = container;
            _middleCenterContainer = middleCenterContainer;
            _stretchContainer = stretchContainer;
        }

        public GameObject Create(GameObject prefab, ContainerType containerType)
        {
            var container = containerType switch
            {
                ContainerType.MiddleCenter => _middleCenterContainer,
                ContainerType.Stretch => _stretchContainer,
                _ => _middleCenterContainer
            };
            
            return _container.InstantiatePrefab(prefab, container);
        }
    }

    public enum ContainerType
    {
        MiddleCenter,
        Stretch
    }
}