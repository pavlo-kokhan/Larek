using UnityEngine;

namespace Characters
{
    public class CharacterInteractionIndicator : MonoBehaviour
    {
        [SerializeField] private GameObject _indicator;

        private void Start()
        {
            DeactivateIndicator();
        }

        public void ActivateIndicator()
        {
            _indicator.SetActive(true);
        }

        public void DeactivateIndicator()
        {
            _indicator.SetActive(false);
        }
    }
}