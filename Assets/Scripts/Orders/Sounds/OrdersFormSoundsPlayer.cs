using Orders.Input;
using UnityEngine;

namespace Orders.Sounds
{
    public class OrdersFormSoundsPlayer : MonoBehaviour
    {
        [SerializeField] private OrderFormInput _orderFormInput;
        [SerializeField] private AudioClip _pageTurningSound;

        private AudioSource _audioSource;

        private void OnEnable()
        {
            _orderFormInput.PageTurned += OnPageTurned;
        }

        private void OnDisable()
        {
            _orderFormInput.PageTurned -= OnPageTurned;
        }

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnPageTurned(bool isHumanPage)
        {
            _audioSource.PlayOneShot(_pageTurningSound);
        }
    }
}