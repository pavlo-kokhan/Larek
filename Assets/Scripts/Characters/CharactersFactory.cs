using UnityEngine;
using Zenject;

namespace Characters
{
    public class CharactersFactory : IFactory<GameObject, GameObject>
    {
        private readonly DiContainer _container;
        private readonly Transform _charactersContainer;
        private readonly Transform _spawnPoint;
        private readonly Transform _interactionPoint;
        private readonly Transform _leavePoint;

        public CharactersFactory(DiContainer container, 
            Transform charactersContainer, 
            Transform spawnPoint, 
            Transform interactionPoint, 
            Transform leavePoint)
        {
            _container = container;
            _charactersContainer = charactersContainer;
            _spawnPoint = spawnPoint;
            _interactionPoint = interactionPoint;
            _leavePoint = leavePoint;
        }

        public GameObject Create(GameObject prefab)
        {
            var character = _container.InstantiatePrefab(prefab, _charactersContainer);
            var characterMovement = character.GetComponent<CharacterMovement>();
            
            characterMovement.Initialize(_spawnPoint, _interactionPoint, _leavePoint);
            characterMovement.MoveToInteractionPoint();
            
            return character;
        }
    }
}