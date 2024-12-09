using UnityEngine;

namespace Characters
{
    public class CharactersFactory
    {
        private readonly GameObject _characterPrefab;
        private readonly Transform _spawnPoint;
        private readonly Transform _interactionPoint;
        private readonly Transform _leavePoint;

        public CharactersFactory(GameObject characterPrefab, Transform spawnPoint, Transform interactionPoint, Transform leavePoint)
        {
            _characterPrefab = characterPrefab;
            _spawnPoint = spawnPoint;
            _interactionPoint = interactionPoint;
            _leavePoint = leavePoint;
        }

        public GameObject SpawnCharacter(Transform parent)
        {
            var character = Object.Instantiate(_characterPrefab, parent);
            var characterMovement = character.GetComponent<CharacterMovement>();
            
            characterMovement.Initialize(_spawnPoint, _interactionPoint, _leavePoint);
            characterMovement.MoveToInteractionPoint();
            
            return character;
        }
    }
}