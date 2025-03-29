using UnityEngine;
using Zenject;

namespace Characters
{
    public class CharacterSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        
        private CharactersFactory _factory;

        [Inject]
        private void Construct(CharactersFactory factory)
        {
            _factory = factory;
        }
        
        public void SpawnTestCharacter()
        {
            _factory.Create(_prefab);
            gameObject.SetActive(false);
        }
    }
}