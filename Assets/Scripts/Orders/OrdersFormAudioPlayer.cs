using GlobalAudio;
using UnityEngine;
using Zenject;

namespace Orders
{
    public class OrdersFormAudioPlayer : MonoBehaviour
    {
        [SerializeField] private OrderFormInput _orderFormInput;
        [SerializeField] private AudioClip _pageTurningSound;

        [Inject] private GlobalAudioPlayer _globalAudioPlayer;

        private void OnEnable()
        {
            _orderFormInput.PageTurned += OnPageTurned;
        }

        private void OnDisable()
        {
            _orderFormInput.PageTurned -= OnPageTurned;
        }

        private void OnPageTurned(bool isHumanPage)
        {
            _globalAudioPlayer.PlaySoundEffect(_pageTurningSound);
        }
    }
}