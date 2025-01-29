using UnityEngine;
using Zenject;

namespace Characters
{
    public class CharacterSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [Inject] private CharactersFactory _factory;

        public void SpawnTestCharacter()
        {
            _factory.Create(_prefab);
        }
    }
}