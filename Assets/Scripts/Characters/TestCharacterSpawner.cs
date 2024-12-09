using UnityEngine;

namespace Characters
{
    public class TestCharacterSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _characterPrefab;
        [SerializeField] private Transform _parent;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _interactionPoint;
        [SerializeField] private Transform _leavePoint;

        private CharactersFactory _factory;

        private void Awake()
        {
            _factory = new CharactersFactory(_characterPrefab, _spawnPoint, _interactionPoint, _leavePoint);
        }

        public void SpawnTestCharacter()
        {
            var character = _factory.SpawnCharacter(_parent);
            Debug.Log(character.ToString());
        }
    }
}