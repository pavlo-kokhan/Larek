using UnityEngine;

namespace Orders
{
    public class OrdersFormSoundsPlayer : MonoBehaviour
    {
        [SerializeField] private OrderFormInput orderFormInput;
        [SerializeField] private AudioClip pageTurningSound;

        private AudioSource _audioSource;

        private void OnEnable()
        {
            orderFormInput.PageTurned += OnPageTurned;
        }

        private void OnDisable()
        {
            orderFormInput.PageTurned -= OnPageTurned;
        }

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnPageTurned(bool isHumanPage)
        {
            _audioSource.PlayOneShot(pageTurningSound);
        }
    }
}