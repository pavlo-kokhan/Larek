using UnityEngine;
using Zenject;

namespace Panels
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

        // private GameObject GetPrefabForType(InteractivePanelType type)
        // {
        //     switch (type)
        //     {
        //         case InteractivePanelType.Radio:
        //             return Resources.Load<GameObject>("Prefabs/InteractiveObjects/Radio/RadioPanel");
        //         case InteractivePanelType.Calculator:
        //             return null;
        //         case InteractivePanelType.Notebook:
        //             return null;
        //         case InteractivePanelType.OrdersPanel:
        //             return null;
        //         default:
        //             return null;
        //     }
        // }
    }
}