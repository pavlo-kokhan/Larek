using UnityEngine;
using Zenject;

namespace Characters
{
    public class CharactersFactory : IFactory<GameObject, GameObject>
    {
        private readonly DiContainer _container;
        private readonly Transform _charactersContainer;
        private readonly Transform _spawnPoint;

        public CharactersFactory(DiContainer container, Transform charactersContainer, Transform spawnPoint)
        {
            _container = container;
            _charactersContainer = charactersContainer;
            _spawnPoint = spawnPoint;
        }

        public GameObject Create(GameObject prefab)
        {
            var character = _container.InstantiatePrefab(prefab, _charactersContainer);
            character.transform.position = _spawnPoint.position;
            
            return character;
        }
    }
}